namespace dBASE.NET.Tests.Encoders
{
    using System.Text;

    using dBASE.NET.Encoders;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CharacterEncoderTests
    {
        private readonly DbfField characterField = new DbfField("NOMDOC", DbfFieldType.Character, 10);

        private readonly Encoding encoding = Encoding.ASCII;

        [TestMethod]
        public void EncodeTestStringShort()
        {
            // Arrange.
            const string val = "010112.01";
            var expectedVal = new[] { '0', '1', '0', '1', '1', '2', '.', '0', '1', ' ' };

            // Act.
            var expectedEncodedVal = encoding.GetBytes(expectedVal);
            var encodedVal = CharacterEncoder.Instance.Encode(characterField, val, encoding);

            // Assert.
            for (int i = 0; i < characterField.Length; i++)
            {
                Assert.AreEqual(expectedEncodedVal[i], encodedVal[i], $"Position `{i}` failed.");
            }
        }

        [TestMethod]
        public void EncodeTestStringLong()
        {
            // Arrange.
            const string val = "04072016.280";
            var expectedVal = new[] { '0', '4', '0', '7', '2', '0', '1', '6', '.', '2' };

            // Act.
            var expectedEncodedVal = encoding.GetBytes(expectedVal);
            var encodedVal = CharacterEncoder.Instance.Encode(characterField, val, encoding);

            // Assert.
            for (int i = 0; i < characterField.Length; i++)
            {
                Assert.AreEqual(expectedEncodedVal[i], encodedVal[i], $"Position `{i}` failed.");
            }
        }

        [TestMethod]
        public void EncodeTestStringNull()
        {
            // Arrange.
            const string val = null;
            string expectedVal = characterField.DefaultValue;

            // Act.
            var expectedEncodedVal = encoding.GetBytes(expectedVal);
            var encodedVal = CharacterEncoder.Instance.Encode(characterField, val, encoding);

            // Assert.
            for (int i = 0; i < characterField.Length; i++)
            {
                Assert.AreEqual(expectedEncodedVal[i], encodedVal[i], $"Position `{i}` failed.");
            }
        }
    }
}