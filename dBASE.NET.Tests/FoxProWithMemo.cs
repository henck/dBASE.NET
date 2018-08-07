using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	[TestClass]
	public class FoxProWithMemo
	{
		private Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/f5/dbase_f5.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(975, dbf.Records.Count, "Should read 975 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(59, dbf.Fields.Count, "Should read 59 fields.");
		}

		[TestMethod]
		public void MemoTest()
		{
			string text = (string) dbf.Records[1][57];
			Assert.AreEqual("El meu pare.\r\nGuerra: \r\n- hi va per sant joan del 1937\r\n-26 Div, 120 Brig, 1r Bat, m?quines d'acompanyament\r\n- hivern 1938-1939 passa la frontera per Puigcerd?-Burg Madame, formats\r\n- Burg Madame 15-20 dies\r\n- a peu cap a Mont Lluis 1 mes\r\n- amb camions a Vernet d'Ariege, camp de concentraci? a 1 km del riu\r\n- t? els tifus 40 dies\r\n- torna al vendrell per sant joan del 1939\r\nDefunci?:\r\n- a partir dels 80 anys t? arr?tmia i es controla\r\n- als 84 anys es comen?a a aprimar, es preocupa, t? an?mia i li recepten ferro, li fan una endosc?pia i li diagnostiquen vorsitis que li provoquen les hemorr?gies anals. tamb? li treuen l'aspirina del tractament del cor ja que tamb? pot ser llaga a l'est?mag\r\n- despr?s de 6 mesos de prendre ferro i no arreglar-se el problema, jo insisteixo al dr. per? (del ferro), que quina ?s la causa del problema i l'envia al de digestiu.\r\n- li fan una segona endosc?pia i aquesta d?na com a resultat c?ncer de colon, s'ha d'operar: 27-9-1997\r\n- es cansa molt per? surt de casa\r\n- li fan transfusions cada 2-3 setmanes\r\n- al cap d'un any recau i ja no surt de casa\r\n- continuem amb les transfusions, est? molt l?cid\r\n- les pastilles pel dolor ja no s?n suficients, la dra. barba li recepta morfina: comencem amb 0,5 ml cada 4 hores. Cada setmana s'ha d'augmentar, arribem a 4,5 ml\r\n- a finals de gener del 1999 se li nota que va perdent, ja no pot concentrar-se a passar els comptes del banc, em diu que l'ajudi... ja no reprendrem aquesta feina.\r\n- necessita ajuda per anar al lavabo i vol fer pix dret com sempre, li hem d'aguantar l'ampola. Menja sol.\r\n- al cap d'una setmana es nota que perd, els fills proposem que vingui una dona a la nit i de dia, per? la mama s'hi oposa. Durant el dia hi som un fill, el papa necessita ajuda per menjar i anar al lavabo.\r\n- durant la segona setmana de febrer es nota que perd dia a dia, comen?a a tenir dificutat a parlar i se li ha de donar el l?quid amb un gotet de canalla petita amb forats\r\n- el 14 de febrer em quedo a dormir\r\n- el 15 se li ha de donar el menjar amb xeringa grossa amb sonda pel nas\r\n- el 15 a la tarda est? al llit, vol anar al lavabo i en fer for?a per aixecar-se, jo i l'anna l'ajudavem, s'ho fa als llen?ols, el netegem i li posem volquers. Al vespre v?nen el joan i el jordi, els dic que li diguin coses que els ent?n, tot i que ell no diu res, li veig la llengua com entumida\r\n- a les 10 quan vaig a martxar se li humitegen els ulls, els li asseco com quan la mama m'ho feia quan era petit, escalfant el mocador amb la boca, me'n vaig, avui es queda la montse\r\n- a les 2 de la nit la mama, que ha volgut dormir al seu costat aquests ?ltims dies el nota imm?bil i crida a la montse i ella ens truca a la carmen i a mi.\r\n- no ha donat cap feina grossa", text, "Wrong data in memo field");
		}

	  [TestMethod]
		public void LastRecord()
		{
			Assert.AreEqual("tarragona", dbf.Records[974][50], "String in last record does not match.");
		}
	}
}
