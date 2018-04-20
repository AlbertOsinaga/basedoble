using System.Collections.Generic;
using System.Dynamic;
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
        ///                         las propiedades omitidas se asignan con valores default.</param>
        public abstract void FromJson(string jentity);              

        /// <summary>
        /// ParseJson. Toma los valores de propiedades de una entidad (string en formato Json) 
        ///             y los asigna a las propiedades correspondientes de la entidad. 
        /// </summary>
        /// <param name="jentity">Incluye las propiedades de la entidad (formato Json), 
        ///                         las propiedades omitidas se ignoran y no modifican las del objeto entidad.</param>
        public abstract void ParseJson(string jentity);             // Fields en jentity se asignan, los otros se ignoran  

        /// <summary>
        /// ToJson. Toma los valores de propiedades del objeto entidad 
        ///             y los devuelve en un string en formato Json.
        /// </summary>
        /// <returns>String con los valores de las propiedades en formato Json</returns>
        /// <param name="jfields">Campos a incluir en el resultado, 
        ///                       campo "Id" se incluye por defecto,
        ///                       jfield = null => se incluyen todos los campos.</param>
        public abstract string ToJson(string jfields = null);       // Con Id , Incluye fields en jfields (null incluye todos)

        /// <summary>
        /// ToJsonNoid.
        /// </summary>
        /// <returns>The json noid.</returns>
        /// <param name="jfields">Jfields.</param>
        public abstract string ToJsonNoid(string jfields = null);   // No Id , Incluye fields en jfields (null incluye todos) 

        /// <summary>
        /// ToJsonX.
        /// </summary>
        /// <returns>The json x.</returns>
        /// <param name="jfields">Jfields.</param>
        public abstract string ToJsonX(string jfields = null);      // Con Id , eXcluye fields en jfields (null no excluye nada)

        /// <summary>
        /// ToJsonXnoid.
        /// </summary>
        /// <returns>The json xnoid.</returns>
        /// <param name="jfields">Jfields.</param>
        public abstract string ToJsonXnoid(string jfields = null);  // No Id , eXclude fields en jfields (null no excluye nada)

        /// <summary>
        /// ToArrayOfJsonEntities.
        /// </summary>
        /// <returns>Array de strings (formato Json) con un elemento por cada entidad 
        ///             de la colección de entidades en jentities (formato Json).</returns>
        /// <param name="jentities">Array o colección de entidades en formato Json.</param>
        public static string[] ToArrayOfJsonEntities(string jentities)
        {
            List<string> strEntities = new List<string>();
            StringReader sr = new StringReader(jentities);
            JsonTextReader jReader = new JsonTextReader(sr);
            string jentity = string.Empty;
            int indexStart = -1;
            int indexEnd = -1;
            while(jReader.Read())
            {
                if(jReader.TokenType == JsonToken.StartObject)
                {
                    indexStart = jReader.LinePosition - 1;
                }
                else if (jReader.TokenType == JsonToken.EndObject)
                {
                    indexEnd = jReader.LinePosition;
                    jentity = jentities.Substring(indexStart, indexEnd - indexStart);
                    strEntities.Add(jentity);
                    indexStart = -1;
                    indexEnd = -1;
                    jentity = string.Empty;
                }
            }
            jReader.Close();
            sr.Close();

            return strEntities.ToArray();
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
