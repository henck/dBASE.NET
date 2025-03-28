using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET {
    public class VFPMemoHeader {
        private int BlockSize = 64;

        public void Create(Stream sw) {
            using (BinaryWriter writer = new BinaryWriter(sw, Encoding.ASCII, false)) {
                /*
                Byte offset Description
                00 – 03 Location of next free block (Big Endian)
                04 – 05 Unused
                06 – 07 Block size(bytes per block) (Big Endian)
                08 – 511 Unused
                */

                writer.Write(BitConverter.GetBytes(0x08).Reverse().ToArray()); // Location of next free block (8 = 512/BlockSize) in Big Endian
                writer.Write((short)0x00); // Unused
                writer.Write(BitConverter.GetBytes((short)BlockSize).Reverse().ToArray()); // Block size in Big Endian
                for (int i = 0; i < 504; i++) {
                    writer.Write((byte)0x00); // Unused
                } // 0x08 - 0x1FF
                // 0x200 First free block
            }
        }
    }
}
