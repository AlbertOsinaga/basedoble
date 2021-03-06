﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace base2.Modelo
{
    /// <summary>
    /// Entity. Clase base para las clases POCO del modelo EF Core "base2".
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// FromJson. Toma los valores de propiedades de una entidad (string en formato Json) 
        ///             y los asigna a las propiedades correspondientes de la entidad. 
        /// </summary>
        /// <param name="jentity">Incluye las propiedades de la entidad (formato Json), 
        ///                         las propiedades omitidas indican que las propiedades correspondientes del objeto entidad
        ///                         se debenr reaasignar con valores default.</param>
        public abstract void FromJson(string jentity);              

        /// <summary>
        /// ParseJson. Toma los valores de propiedades de una entidad (string en formato Json) 
        ///             y los asigna a las propiedades correspondientes de la entidad. 
        /// </summary>
        /// <param name="jentity">Incluye las propiedades de la entidad (formato Json), 
        ///                         las propiedades omitidas se ignoran y no se modifican las propiedades correspondientes 
        ///                         del objeto entidad.</param>
        public abstract void ParseJson(string jentity);  

        /// <summary>
        /// ToJson. Toma los valores de propiedades del objeto entidad 
        ///             y los devuelve en un string en formato Json.
        /// </summary>
        /// <returns>String con los valores de las propiedades en formato Json</returns>
        /// <param name="jfields">Campos a incluir en el resultado, 
        ///                       campo "Id" se incluye por defecto,
        ///                       jfield = null => se incluyen todos los campos.</param>
        public abstract string ToJson(string jfields = null);

        /// <summary>
        /// ToJsonNoid.  Toma los valores de propiedades del objeto entidad 
        ///             y los devuelve en un string en formato Json.
        /// </summary>
        /// <returns>String con los valores de las propiedades en formato Json.</returns>
        /// <param name="jfields">Campos a incluir en el resultado, 
        ///                       campo "Id" no se incluye por defecto,
        ///                       jfield = null => se incluyen todos los campos.</param>
        public abstract string ToJsonNoid(string jfields = null);

        /// <summary>
        /// ToJsonX. Toma los valores de propiedades del objeto entidad 
        ///             y los devuelve en un string en formato Json.
        /// </summary>
        /// <returns>String con los valores de las propiedades en formato Json.</returns>
        /// <param name="jfields">Campos a excluir en el resultado, 
        ///                       campo "Id" se incluye por defecto,
        ///                       jfield = null => se incluyen todos los campos.</param>
        public abstract string ToJsonX(string jfields = null);

        /// <summary>
        /// ToJsonXnoid.  Toma los valores de propiedades del objeto entidad 
        ///             y los devuelve en un string en formato Json.
        /// </summary>
        /// <returns>String con los valores de las propiedades en formato Json.</returns>
        /// <param name="jfields">Campos a excluir en el resultado, 
        ///                       campo "Id" no se incluye por defecto,
        ///                       jfield = null => se incluyen todos los campos.</param>
        public abstract string ToJsonXnoid(string jfields = null);

        /// <summary>
        /// SplitJsonObjects.
        /// </summary>
        /// <returns>Array de strings (formato Json) con un elemento por cada Objeto 
        ///             de la colección de objetos en jobjects (formato Json).</returns>
        /// <param name="jobjects">Array o colección de objects en formato Json.</param>
        public static string[] SplitJsonObjects(string jobjects)
        {
            if (string.IsNullOrWhiteSpace(jobjects))
                throw new NullReferenceException("Argumento nulo al llamar a Entity.SplitJsonObjects.");

            try
            {
                List<string> strObjects = new List<string>();
                StringReader sr = new StringReader(jobjects);
                JsonTextReader jReader = new JsonTextReader(sr);
                string jobject = string.Empty;
                int indexStart = -1;
                int indexEnd = -1;
                while (jReader.Read())
                {
                    if (jReader.TokenType == JsonToken.StartObject)
                    {
                        indexStart = jReader.LinePosition - 1;
                    }
                    else if (jReader.TokenType == JsonToken.EndObject)
                    {
                        indexEnd = jReader.LinePosition;
                        jobject = jobjects.Substring(indexStart, indexEnd - indexStart);
                        strObjects.Add(jobject);
                        indexStart = -1;
                        indexEnd = -1;
                        jobject = string.Empty;
                    }
                }
                jReader.Close();
                sr.Close();

                return strObjects.ToArray();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Entity.SplitJsonObjects.", ex);
            }
        }

        /// <summary>
        /// ToJson. Convierte el array de entidades a un string en formato Json. 
        ///         Para ser usado mediante ToJson, ToJsonNoid, ToJsonX, ToJsonXnoid
        /// </summary>
        /// <returns>string con array de entidades en formato json.</returns>
        /// <param name="entities">Array de entidades a ser codificadas en formato json.</param>
        /// <param name="jfields">Enumeración de campos de la entidad, en formato json,
        ///                             a incluir o excluir de acuerdo a sufijo.</param>
        /// <param name="sufijo">El sufijo puede ser: null o algun otro valor fuera de lista (incluye todos los campos). 
        ///                                             "Noid": No incluye id, incluye campos de la lista json.
        ///                                             "X": Incluye id, eXcluye campos de la lista json.
        ///                                             "Xnoid": Noincluye id, eXcluye campos de la lista json.</param>
        public static string ToJson(Entity[] entities, string jfields = null, string sufijo = null)
        {
            StringBuilder json = new StringBuilder();
            StringWriter sw = new StringWriter(json);
            JsonTextWriter jw = new JsonTextWriter(sw);
            jw.WriteStartArray();
            for (int i = 0; i < entities.Length; i++)
            {
                if(sufijo == null)
                    jw.WriteRaw(entities[i].ToJson(jfields));
                else if(sufijo == "Noid")
                    jw.WriteRaw(entities[i].ToJsonNoid(jfields));
                else if (sufijo == "X")
                    jw.WriteRaw(entities[i].ToJsonX(jfields));
                else if (sufijo == "Xnoid")
                    jw.WriteRaw(entities[i].ToJsonXnoid(jfields));
                else
                    jw.WriteRaw(entities[i].ToJson(jfields));

                if (i < entities.Length - 1)
                    jw.WriteRaw(",");
            }
            jw.WriteEndArray();
            jw.Close();
            sw.Close();
            return json.ToString();
        }

        /// <summary>
        /// ToJsonNoid. Convierte el array de entities a un string en formato Json.
        ///             Equivalente a llamar ToJson(entities, fields, "Noid").
        /// </summary>
        /// <returns>string con array de entidades en formato json.</returns>
        /// <param name="entities">Array de entidades a ser codificadas en formato json.</param>
        /// <param name="jfields">Enumeración de campos de la entidad, en formato json,
        ///                             a incluir (null incluye todos los campos).</param>
        public static string ToJsonNoid(Entity[] entities, string jfields = null)
        {
            return Entity.ToJson(entities, jfields, "Noid");
        }

        /// <summary>
        /// ToJsonX. Convierte el array de entities a un string en formato Json.
        ///             Equivalente a llamar ToJson(entities, fields, "X").
        /// </summary>
        /// <returns>string con array de entidades en formato json.</returns>
        /// <param name="entities">Array de entidades a ser codificadas en formato json.</param>
        /// <param name="jfields">Enumeración de campos de la entidad, en formato json,
        ///                             a eXcluir (null no eXcluye ningún campo).
        ///                             Incluye campo "id" si no está en lista de campos a excluir</param>
        public static string ToJsonX(Entity[] entities, string jfields = null)
        {
            return Entity.ToJson(entities, jfields, "X");
        }

        /// <summary>
        /// ToJsonXnoid. Convierte el array de entities a un string en formato Json.
        ///             Equivalente a llamar ToJson(entities, fields, "Xnoid").
        /// </summary>
        /// <returns>string con array de entidades en formato json.</returns>
        /// <param name="entities">Array de entidades a ser codificadas en formato json.</param>
        /// <param name="jfields">Enumeración de campos de la entidad, en formato json,
        ///                             a eXcluir (null no eXcluye ningún campo).
        ///                             No incluye campo "id" aún si no está en lista de campos a excluir</param>
        public static string ToJsonXnoid(Entity[] entities, string jfields = null)
        {
            return Entity.ToJson(entities, jfields, "Xnoid");
        }
    }
}
