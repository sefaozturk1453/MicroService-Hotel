using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Helpers;

namespace Hotel.Test
{
    public class TypeHelperTests
    {

        [Test]
        public void enum_Kontrol_Edildi_mi()
        {
            // Arrenge

            var sayi = 2;
            var beklenen = true;


            // Action

            var gerceklesen = TypeHelper.InfoTypeCheck(sayi);



            // Assert

            Assert.AreEqual(beklenen, gerceklesen);

        }
    }
}
