﻿using System;
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
        public Moneda(string jentity) : this()
        {
            FromString(jentity);
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
            Moneda entity = null;
            try
            {
                entity = JsonConvert.DeserializeObject<Moneda>(jentity);
                this.MonedaId = entity.MonedaId;
                this.Simbolo = entity.Simbolo;
                this.Nombre = entity.Nombre;
                this.Tipo = entity.Tipo;
                this.TasaCambio = entity.TasaCambio;
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

                dynamic dynEntity = new ExpandoObject();
                if (noid == false)
                    dynEntity.MonedaId = MonedaId;
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
                                    dynEntity.MonedaId = MonedaId;
                                break;
                            case "simbolo":
                                dynEntity.Simbolo = Simbolo;
                                break;
                            case "nombre":
                                dynEntity.Nombre = Nombre;
                                break;
                            case "tipo":
                                dynEntity.Tipo = Tipo;
                                break;
                            case "tasacambio":
                                dynEntity.TasaCambio = TasaCambio;
                                break;
                            default:
                                throw new ArgumentException($"Error en jfields, '{field}' no existe en entidad");
                        }
                    }
                }
                return JsonConvert.SerializeObject(dynEntity);
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

                dynamic fieldsEntity = new ExpandoObject();
                fieldsEntity.MonedaId = !noid;
                fieldsEntity.Simbolo = true;
                fieldsEntity.Nombre = true;
                fieldsEntity.Tipo = true;
                fieldsEntity.TasaCambio = true;
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.String)
                    {
                        string field = jreader.Value as String;
                        switch (field.Trim().ToLower())
                        {
                            case "id":
                            case "monedaid":
                                fieldsEntity.MonedaId = false;
                                break;
                            case "simbolo":
                                fieldsEntity.Simbolo = false;
                                break;
                            case "nombre":
                                fieldsEntity.Nombre = false;
                                break;
                            case "tipo":
                                fieldsEntity.Tipo = false;
                                break;
                            case "tasacambio":
                                fieldsEntity.TasaCambio = false;
                                break;
                            default:
                                throw new ArgumentException($"Error en jfields, '{field}' no existe en entidad");
                        }
                    }
                }

                dynamic dynEntity = new ExpandoObject();
                if (fieldsEntity.MonedaId)
                    dynEntity.MonedaId = MonedaId;
                if (fieldsEntity.Simbolo)
                    dynEntity.Simbolo = Simbolo;
                if (fieldsEntity.Nombre)
                    dynEntity.Nombre = Nombre;
                if (fieldsEntity.Tipo)
                    dynEntity.Tipo = Tipo;
                if (fieldsEntity.TasaCambio)
                    dynEntity.TasaCambio = TasaCambio;

                return JsonConvert.SerializeObject(dynEntity);
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
            string[] strEntities = Entity.SplitJsonObjects(jentities);
            List<Moneda> entities = new List<Moneda>();
            foreach (var sentity in strEntities)
            {
                Moneda entity = new Moneda(sentity);
                entities.Add(entity);
            }

            return entities.ToArray();
        }
    }
}
