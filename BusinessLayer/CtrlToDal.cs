using BeddestDAL;
using IotMessagesReceiver;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class CtrlToDal
    {
        #region Users
        public static UserDTO LogIn(string name, string password)
        {
            User user = UserDataAccess.FindUser(name, password);
            if (user != null)
                return user.ToDto();
            else
                return null;
        }

        public static void StartReceiving()
        {
            Transfer.StartReceiving();
        }

        public static UserDTO Register(string newUserName, string password)
        {
            UserDTO result = null;
            bool userExists = UserDataAccess.CheckUserExists(newUserName);
            if (!userExists)
            {
                UserDataAccess.AddUser(newUserName, password);
                result = LogIn(newUserName, password);
            }
            return result;
        }
        #endregion

        #region Beds
        public static BedDTO GetBed(int bedId)
        {
            Bed bed = BedDataAccess.GetBed(bedId);
            if (bed == null)
                return null;
            List<Block> blocks = BlockDataAccess.GetBlocks(bedId);
            int[] blocksDTO = new int[blocks.Count];
            for (int j = 0; j < blocks.Count; j++)
                blocksDTO[j] = blocks[j].BlockId;
            return bed.ToDto(blocksDTO);
        }

        public static List<BedDTO> GetBeds(int userId)
        {
            List<Bed> userBeds = BedDataAccess.GetBeds(userId);

            int bedsCount = userBeds.Count();
            List<BedDTO> result = new List<BedDTO>();
            foreach (Bed bed in userBeds)
            {
                List<Block> blocks = BlockDataAccess.GetBlocks(bed.BedId);
                int[] blocksDTO = new int[blocks.Count];
                for (int j = 0; j < blocks.Count; j++)
                    blocksDTO[j] = blocks[j].BlockId;
                result.Add(bed.ToDto(blocksDTO));
            }
            return result;
        }

        public static List<BedDTO> GetAllBeds()
        {
            List<Bed> userBeds = BedDataAccess.GetAllBeds();

            int bedsCount = userBeds.Count();
            List<BedDTO> result = new List<BedDTO>();
            result.AddRange(userBeds.Select(x => x.ToDto(new int[4])));

            return result;
        }

        public static async Task SetTemp(int bedId, int newTemp)
        {
            string deviceId = DeviceDataAccess.GetDeviceName(bedId);
            if (deviceId == "")
                return;

            await Transfer.SendC2DAsync(deviceId, "SetTemp", newTemp);
            BedDataAccess.UpdateTemperature(bedId, newTemp);
        }

        public static async Task SetHeatingTime(int bedId, int newTime)
        {
            string deviceId = DeviceDataAccess.GetDeviceName(bedId);
            if (deviceId == "")
                return;

            await Transfer.SendC2DAsync(deviceId, "SetTime", newTime);
            BedDataAccess.UpdateHeatingTime(bedId, newTime);
        }

        public static async Task AddBed(int userId, int[] hardnessLevels)
        {
            Bed newBed = new Bed(userId);
            int bedId = BedDataAccess.AddBed(newBed);

            int blocksCount = 4;
            Block[] blocks = new Block[blocksCount];
            for (int i = 0; i < blocksCount; i++)
            {
                blocks[i] = new Block(bedId, hardnessLevels[i]);
            }
            BlockDataAccess.AddBlocks(blocks);

            await AddDeviceAsync(bedId);
        }

        public static async Task RemoveBed(int bedId)
        {
            string deviceName = DeviceDataAccess.GetDeviceName(bedId);
            bool removed = await IotCore.RemoveDeviceAsync(deviceName);
            if (removed)
            {
                BlockDataAccess.RemoveBlocks(bedId);
                BedDataAccess.RemoveBed(bedId);
            }
        }

        #endregion

        #region Modes
        public static List<ModeDTO> GetModes(int userId)
        {
            List<Mode> userModes = ModeDataAccess.GetModes(userId),
                commonModes = ModeDataAccess.GetModes(1);

            int userModesCount = userModes.Count,
                commonModesCount = commonModes.Count;
            List<ModeDTO> result = new List<ModeDTO>();
            for (int i = 0; i < userModesCount; i++)
            {
                result.Add(userModes[i].ToDto());
            }
            for (int i = 0; i < commonModesCount; i++)
            {
                result.Add(commonModes[i].ToDto());
            }
            return result;
        }

        public static async Task SelectModeForIotAsync(int modeId, int bedId)
        {
            string deviceId = DeviceDataAccess.GetDeviceName(bedId);
            if (deviceId == "")
                return;
            Mode mode = ModeDataAccess.GetMode(modeId);
            ModeDTO dto = new ModeDTO(mode.ModeId, mode.Name, mode.HeadHeight, mode.HeadTilt, mode.HeadHardness,
                mode.LegsHeight, mode.LegsTilt, mode.LegsHardness, mode.OtherHeight, mode.OtherHardness);

            await Transfer.SendC2DAsync(deviceId, "SelectMode", dto);

            /*var tasks = new List<Task>();
            foreach (var eventHubReceiver in Transfer.eventHubReceivers)
            {
                tasks.Add(Transfer.ReceiveMessage(eventHubReceiver));
            }
            Task.WaitAny(tasks.ToArray*/
        }
        public static void SelectModeForDB(int modeId, int bedId, int headIndex, int legsIndex)
        {
            Mode mode = ModeDataAccess.GetMode(modeId);
            List<Block> bedBlocks = BlockDataAccess.GetBlocks(bedId);
            int blocksCount = bedBlocks.Count;
            for (int i = 0; i < blocksCount; i++)
            {
                if (i == headIndex - 1)
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].BlockId, mode.HeadHeight, mode.HeadTilt, mode.HeadHardness);
                }
                else if (i == legsIndex - 1)
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].BlockId, mode.LegsHeight, mode.LegsTilt, mode.LegsHardness);
                }
                else
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].BlockId, mode.OtherHeight, 0, mode.OtherHardness);
                }
            }
        }

        public static async Task AddModeForIotAsync(int userId, int bedId, string name)
        {
            string deviceId = DeviceDataAccess.GetDeviceName(bedId);
            if (deviceId == "")
                return;

            await Transfer.SendC2DAsync(deviceId, "AddMode", name);
        }
        public static void AddModeForDB(int userId, int bedId, string name, int head, int legs)
        { 
            List<Block> blocks = BlockDataAccess.GetBlocks(bedId);
            Mode newMode = Services.CreateMode(userId, head - 1, legs - 1, name, blocks.ToArray());
            ModeDataAccess.CreateMode(newMode);
        }
        #endregion

        #region Blocks

        public static BlockDTO GetBlockData(int blockId)
        {
            Block result = BlockDataAccess.GetBlock(blockId);
            if (result == null)
                return null;
            return result.ToDto();
        }

        public static async Task ChangeBlockAsync(int blockId, int height, int tiltAngle, int hardness)
        {
            int bedId = BedDataAccess.GetBedByBlock(blockId).BedId;

            string deviceId = DeviceDataAccess.GetDeviceName(bedId);
            if (deviceId == "")
                return;
            
            BlockDTO dto = new BlockDTO(blockId, height, tiltAngle, hardness);
            await Transfer.SendC2DAsync(deviceId, "ChangeBlock", dto);
            Block block = new Block(dto);
            BlockDataAccess.ChangeBlock(blockId, height, tiltAngle, block.Hardness);
        }

        #endregion

        #region Devices
        public static async Task AddDeviceAsync(int bedId)
        {
            Device newDevice = new Device(bedId);
            DeviceDataAccess.AddDevice(newDevice);

            string clientDeviceKey = await IotCore.AddDeviceAsync(newDevice.DeviceName);
            DeviceDataAccess.SetClientDeviceKey(newDevice.DeviceId, clientDeviceKey);
        }

        public static List<DeviceDTO> GetDevices()
        {
            List<Device> devices = DeviceDataAccess.GetDevices();
            List<DeviceDTO> result = devices.Select(x => new DeviceDTO(x.DeviceId, x.DeviceName, x.BedId, x.ClientDeviceKey)).ToList();
            return result;
        }
        #endregion
    }
}
