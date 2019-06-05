using System;
using System.Collections.Generic;
using System.Text;
using TestCashdroJsonCore.Enums;
namespace TestCashdroJsonCore.Implementation
{
    public class ResponseListRestDM<TData>
    {
        /// <summary>
        /// Constructor para asignar propiedades manualmente
        /// </summary>
        public ResponseListRestDM()
        { }

        /// <summary>
        /// Constructor para respuestas exitosas.
        /// </summary>
        public ResponseListRestDM(List<TData> data, string mensaje = "OK")
            : this(CashdroEnum.OK, mensaje, data)
        { }

        /// <summary>
        /// Constructor para respuestas con código explícito (ej.: error/fatal).
        /// </summary>
        public ResponseListRestDM(CashdroEnum codigo, string mensaje, List<TData> data = default(List<TData>), string idOperacion = "0")
        {
            Codigo = codigo;
            Mensaje = mensaje;
            Data = data;
        }

        public CashdroEnum Codigo { get; set; }

        public string Mensaje { get; set; }

        public List<TData> Data { get; set; }
    }

    public class ResponseRestDM<TData>
    {
        /// <summary>
        /// Constructor para asignar propiedades manualmente
        /// </summary>
        public ResponseRestDM()
        { }

        /// <summary>
        /// Constructor para respuestas exitosas.
        /// </summary>
        public ResponseRestDM(TData data, string mensaje = "OK")
            : this(CashdroEnum.OK, mensaje, data)
        { }

        /// <summary>
        /// Constructor para respuestas con código explícito (ej.: error/fatal).
        /// </summary>
        public ResponseRestDM(CashdroEnum codigo, string mensaje, TData data = default(TData), string idOperacion = "0")
        {
            Codigo = codigo;
            Mensaje = mensaje;
            Data = data;
        }

        public CashdroEnum Codigo { get; set; }

        public string Mensaje { get; set; }

        public TData Data { get; set; }
    }
}
