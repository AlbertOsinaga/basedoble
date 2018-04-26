using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Microsoft.CSharp;
using Newtonsoft.Json;

namespace base2.Modelo
{
    /// <summary>
    /// Moneda. Clase Entidad, ejemplifica una clase POCO, derivada de Entity, del modelo EF Core "base2".
    ///                         Representa una entidad "Moneda" para manejo de monedas en una solución
    ///                         de tipo "Contable".
    /// </summary>
    public partial class Moneda : Entity
    {
        public int MonedaId { get; set; }
        public string Simbolo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }            // B-Base X-Ext
        public decimal TasaCambio { get; set; }
	}

    public partial class Moneda
    {
        public Moneda() {}
        public Moneda(string jmoneda) : this()
        {
            FromString(jmoneda);
        }

        public override void FromJson(string jentity)
        {
            FromString(jentity);
        }

        public override void ParseJson(string jentity)
        {
            dynamic dynMoneda = null;
            try
            {
                dynMoneda = JsonConvert.DeserializeObject(jentity);
                if (dynMoneda.MonedaId is object)
                    this.MonedaId = dynMoneda.MonedaId;
                if (dynMoneda.Simbolo is object)
                    this.Simbolo = dynMoneda.Simbolo;
                if (dynMoneda.Nombre is object)
                    this.Nombre = dynMoneda.Nombre;
                if (dynMoneda.Tipo is object)
                    this.Tipo = dynMoneda.Tipo;
                if (dynMoneda.TasaCambio is object)
                    this.TasaCambio = dynMoneda.TasaCambio;
            }
            catch (Exception ex)
            {
                throw (new ApplicationException("Error en 'JsonConvert.DeserializeObject'", ex));
            }
        }

        public override string ToJson(string jfields = null)
        {
            return ToString(jfields, noid: false);
        }

        public override string ToJsonNoid(string jfields = null)
        {
            return ToString(jfields, noid: true);
        }

        public override string ToJsonX(string jfields = null)
        {
            return ToStringX(jfields, noid: false);
        }

        public override string ToJsonXnoid(string jfields = null)
        {
            return ToStringX(jfields, noid: true);
        }

        public void FromString(string jentity)
        {
            Moneda moneda = null;
            try
            {
                moneda = JsonConvert.DeserializeObject<Moneda>(jentity);
                this.MonedaId = moneda.MonedaId;
                this.Simbolo = moneda.Simbolo;
                this.Nombre = moneda.Nombre;
                this.Tipo = moneda.Tipo;
                this.TasaCambio = moneda.TasaCambio;
            }
            catch (Exception ex)
            {
                throw (new ApplicationException("Error en 'JsonConvert.DeserializeObject'", ex));
            }
        }

        public string ToString(string jfields = null, bool noid = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jfields))
                {
                    if (noid == false)
                    {
                        return JsonConvert.SerializeObject(this);
                    }
                    else
                    {
                        return ToStringX("[\"Id\"]");
                    }
                }

                StringReader sreader = new StringReader(jfields);
                JsonTextReader jreader = new JsonTextReader(sreader);

                dynamic dynMoneda = new ExpandoObject();
                if (noid == false)
                    dynMoneda.MonedaId = MonedaId;
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.String)
                    {
                        string field = jreader.Value as String;
                        switch (field.Trim().ToLower())
                        {
                            case "id":
                            case "monedaid":
                                if (noid == true)
                                    dynMoneda.MonedaId = MonedaId;
                                break;
                            case "simbolo":
                                dynMoneda.Simbolo = Simbolo;
                                break;
                            case "nombre":
                                dynMoneda.Nombre = Nombre;
                                break;
                            case "tipo":
                                dynMoneda.Tipo = Tipo;
                                break;
                            case "tasacambio":
                                dynMoneda.TasaCambio = TasaCambio;
                                break;
                            default:
                                throw new ArgumentException($"Error en jfields, '{field}' no existe en entidad");
                        }
                    }
                }
                return JsonConvert.SerializeObject(dynMoneda);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new ApplicationException("Error en 'JsonConvert.SerializeObject'", ex));
            }
        }

        public string ToStringX(string jfields = null, bool noid = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jfields))
                {
                    if (noid == false)
                    {
                        return JsonConvert.SerializeObject(this);
                    }
                    else
                    {
                        return ToStringX("[\"Id\"]");
                    }
                }

                StringReader sreader = new StringReader(jfields);
                JsonTextReader jreader = new JsonTextReader(sreader);

                dynamic fieldsMoneda = new ExpandoObject();
                fieldsMoneda.MonedaId = !noid;
                fieldsMoneda.Simbolo = true;
                fieldsMoneda.Nombre = true;
                fieldsMoneda.Tipo = true;
                fieldsMoneda.TasaCambio = true;
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.String)
                    {
                        string field = jreader.Value as String;
                        switch (field.Trim().ToLower())
                        {
                            case "id":
                            case "monedaid":
                                fieldsMoneda.MonedaId = false;
                                break;
                            case "simbolo":
                                fieldsMoneda.Simbolo = false;
                                break;
                            case "nombre":
                                fieldsMoneda.Nombre = false;
                                break;
                            case "tipo":
                                fieldsMoneda.Tipo = false;
                                break;
                            case "tasacambio":
                                fieldsMoneda.TasaCambio = false;
                                break;
                            default:
                                throw new ArgumentException($"Error en jfields, '{field}' no existe en entidad");
                        }
                    }
                }

                dynamic dynMoneda = new ExpandoObject();
                if (fieldsMoneda.MonedaId)
                    dynMoneda.MonedaId = MonedaId;
                if (fieldsMoneda.Simbolo)
                    dynMoneda.Simbolo = Simbolo;
                if (fieldsMoneda.Nombre)
                    dynMoneda.Nombre = Nombre;
                if (fieldsMoneda.Tipo)
                    dynMoneda.Tipo = Tipo;
                if (fieldsMoneda.TasaCambio)
                    dynMoneda.TasaCambio = TasaCambio;

                return JsonConvert.SerializeObject(dynMoneda);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new ApplicationException("Error en 'JsonConvert.SerializeObject'", ex));
            }
        }

        public static Moneda[] ToArray(string jentities)
        {
            string[] strEntities = Entity.ToArrayOfJsonEntities(jentities);
            List<Moneda> monedas = new List<Moneda>();
            foreach (var se in strEntities)
            {
                Moneda moneda = new Moneda(se);
                monedas.Add(moneda);
            }

            return monedas.ToArray();
        }
    }
}
