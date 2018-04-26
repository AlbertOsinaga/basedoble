using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Microsoft.CSharp;
using Newtonsoft.Json;

namespace base2.Modelo
{
    /// <summary>
    /// Cliente. Clase Entidad, ejemplifica una clase POCO, derivada de Entity, del modelo EF Core "base2".
    ///                         Representa una entidad "Cliente" para manejo de clientes en una solución
    ///                         de tipo "Contable".
    /// </summary>
    public partial class Cliente : Entity
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public decimal Saldo { get; set; }
    }

    public partial class Cliente
    {
        public Cliente() { }
        public Cliente(string jentity) : this()
        {
            FromString(jentity);
        }

        public override void FromJson(string jentity)
        {
            FromString(jentity);
        }

        public override void ParseJson(string jentity)
        {
            dynamic dynCliente = null;
            try
            {
                dynCliente = JsonConvert.DeserializeObject(jentity);
                if (dynCliente.ClienteId is object)
                    this.ClienteId = dynCliente.ClienteId;
                if (dynCliente.Nombre is object)
                    this.Nombre = dynCliente.Nombre;
                if (dynCliente.Telefono is object)
                    this.Telefono = dynCliente.Telefono;
                if (dynCliente.Saldo is object)
                    this.Saldo = dynCliente.Saldo;
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
            Cliente entity = null;
            try
            {
                entity = JsonConvert.DeserializeObject<Cliente>(jentity);
                this.ClienteId = entity.ClienteId;
                this.Nombre = entity.Nombre;
                this.Telefono = entity.Telefono;
                this.Saldo = entity.Saldo;
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
                    dynEntity.ClienteId = ClienteId;
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.String)
                    {
                        string field = jreader.Value as String;
                        switch (field.Trim().ToLower())
                        {
                            case "id":
                            case "clienteid":
                                if (noid == true)
                                    dynEntity.ClienteId = ClienteId;
                                break;
                            case "nombre":
                                dynEntity.Nombre = Nombre;
                                break;
                            case "telefono":
                                dynEntity.Telefono = Telefono;
                                break;
                            case "saldo":
                                dynEntity.Saldo = Saldo;
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
                fieldsEntity.ClienteId = !noid;
                fieldsEntity.Nombre = true;
                fieldsEntity.Telefono = true;
                fieldsEntity.Saldo = true;
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.String)
                    {
                        string field = jreader.Value as String;
                        switch (field.Trim().ToLower())
                        {
                            case "id":
                            case "clienteid":
                                fieldsEntity.ClienteId = false;
                                break;
                            case "nombre":
                                fieldsEntity.Nombre = false;
                                break;
                            case "telefono":
                                fieldsEntity.Telefono = false;
                                break;
                            case "saldo":
                                fieldsEntity.Saldo = false;
                                break;
                            default:
                                throw new ArgumentException($"Error en jfields, '{field}' no existe en entidad");
                        }
                    }
                }

                dynamic dynEntity = new ExpandoObject();
                if (fieldsEntity.ClienteId)
                    dynEntity.ClienteId = ClienteId;
                if (fieldsEntity.Nombre)
                    dynEntity.Nombre = Nombre;
                if (fieldsEntity.Telefono)
                    dynEntity.Telefono = Telefono;
                if (fieldsEntity.Saldo)
                    dynEntity.Saldo = Saldo;

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

        public static Cliente[] ToArray(string jentities)
        {
            string[] strEntities = Entity.SplitJsonObjects(jentities);
            List<Cliente> entities = new List<Cliente>();
            foreach (var sentity in strEntities)
            {
                Cliente entity = new Cliente(sentity);
                entities.Add(entity);
            }

            return entities.ToArray();
        }
    }
}

