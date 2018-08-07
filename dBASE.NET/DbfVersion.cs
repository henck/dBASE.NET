using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	public enum DbfVersion
	{
		Unknown                          = 0x00,
		FoxBase                          = 0x02,
		FoxBaseDBase3NoMemo              = 0x03, // DONE
		VisualFoxPro                     = 0x30, // DONE
		VisualFoxProWithAutoIncrement    = 0x31, // DONE
		dBase4SQLTableNoMemo             = 0x43,
		dBase4SQLSystemNoMemo            = 0x63,
		FoxBaseDBase3WithMemo            = 0x83, // DONE
		dBase4WithMemo                   = 0x8B, // DONE
		dBase4SQLTableWithMemo           = 0xCB,
		FoxPro2WithMemo                  = 0xF5, // DONE
		FoxBASE                          = 0xFB
	}
}
