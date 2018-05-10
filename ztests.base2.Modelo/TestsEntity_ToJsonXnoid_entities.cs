using System;
using Xunit;

using base2.Modelo;

namespace ztests.base2.Modelo
{
    public class TestsEntity_ToJsonXnoid_entities
    {
        [Fact]
        public void TestToJsonXnoid_entities_OK01()
        {
            // Prepara (entidades mismo tipo, jfields nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Moneda moneda2 = new Moneda
            {
                MonedaId = 2,
                Simbolo = "MD",
                Nombre = "Moneda Dos",
                Tipo = "X",
                TasaCambio = 2.3456M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] monedas = { moneda1, moneda2, moneda3 };
            string jentitiesEsperadas = "[" + "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                                    "\"Tipo\":\"B\",\"TasaCambio\":3.4783}," +
                                                "{\"Simbolo\":\"MD\",\"Nombre\":\"Moneda Dos\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":2.3456}," +
                                                "{\"Simbolo\":\"MT\",\"Nombre\":\"Moneda Tres\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":5.8321}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonXnoid(monedas);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonXnoid_entities_OK02()
        {
            // Prepara (entidades distintos tipos, jfields nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Cliente cliente = new Cliente
            {
                ClienteId = 100,
                Nombre = "Mario Bross",
                Telefono = "72007319",
                Saldo = 1200.34M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] entities = { moneda1, cliente, moneda3 };
            string jentitiesEsperadas = "[" + "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                                    "\"Tipo\":\"B\",\"TasaCambio\":3.4783}," +
                                                "{\"Nombre\":\"Mario Bross\"," +
                                                    "\"Telefono\":\"72007319\",\"Saldo\":1200.34}," +
                                                "{\"Simbolo\":\"MT\",\"Nombre\":\"Moneda Tres\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":5.8321}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonXnoid(entities);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonXnoid_entities_OK03()
        {
            // Prepara (array entidades vacío, jfields nulo)
            Entity[] entities = { };
            string jentitiesEsperadas = "[]";

            // Ejecuta
            string jentities = Entity.ToJsonXnoid(entities);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonXnoid_entities_OK04()
        {
            // Prepara (entidades mismo tipo, jfields no nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Moneda moneda2 = new Moneda
            {
                MonedaId = 2,
                Simbolo = "MD",
                Nombre = "Moneda Dos",
                Tipo = "X",
                TasaCambio = 2.3456M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] monedas = { moneda1, moneda2, moneda3 };
            string jfields = "[\"Simbolo\",\"Tipo\"]";
            string jentitiesEsperadas = "[" + "{\"Nombre\":\"Moneda Unica\"," +
                                                    "\"TasaCambio\":3.4783}," +
                                                "{\"Nombre\":\"Moneda Dos\"," +
                                                    "\"TasaCambio\":2.3456}," +
                                                "{\"Nombre\":\"Moneda Tres\"," +
                                                    "\"TasaCambio\":5.8321}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonXnoid(monedas, jfields);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonXnoid_entities_ZBad01()
        {
            // Prepara (array entidades nulo)
            Entity[] entities = null;

            // Ejecuta
            try
            {
                Assert.Throws<NullReferenceException>(() => Entity.ToJsonXnoid(entities));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestToJsonXnoid_entities_ZBad02()
        {
            // Prepara (entidades distintos tipos, jfields nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Cliente cliente = new Cliente
            {
                ClienteId = 100,
                Nombre = "Mario Bross",
                Telefono = "72007319",
                Saldo = 1200.34M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] entities = { moneda1, cliente, moneda3 };
            string jfields = "[\"Nombre\",\"Tipo\"]";

            // Ejecuta
            try
            {
                Assert.Throws<ArgumentException>(() => Entity.ToJsonXnoid(entities, jfields));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }
    }
}
