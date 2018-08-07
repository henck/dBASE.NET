using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class DecoderFactory
	{
		public static IDecoder GetDecoder(DbfFieldType type)
		{
			switch (type)
			{
				case DbfFieldType.Character:
					return CharacterDecoder.Instance;
				case DbfFieldType.Currency:
					return CurrencyDecoder.Instance;
				case DbfFieldType.Date:
					return DateDecoder.Instance;
				case DbfFieldType.DateTime:
					return DateTimeDecoder.Instance;
				case DbfFieldType.Float:
					return FloatDecoder.Instance;
				case DbfFieldType.Integer:
					return IntegerDecoder.Instance;
				case DbfFieldType.Logical:
					return LogicalDecoder.Instance;
				case DbfFieldType.Memo:
					return MemoDecoder.Instance;
				case DbfFieldType.NullFlags:
					return NullFlagsDecoder.Instance;
				case DbfFieldType.Numeric:
					return NumericDecoder.Instance;
				default:
					throw new ArgumentException("No decoder found for dBASE type " + type);
			}
		}
	}
}
