using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.Models
{
    public class ContadorModel
    {
        public int Op1 { get; set; }
        public int Op2 { get; set; }
        public int Op3 { get; set; }
        public int Op4 { get; set; }
        public int Op5 { get; set; }
        public int Op6 { get; set; }
        public int Op7 { get; set; }
        public int Op8 { get; set; }
        public int Op9 { get; set; }
        public int Op10 { get; set; }
        public int Op11 { get; set; }
        public int Op12 { get; set; }
        public int Op13 { get; set; }
        public int Op14 { get; set; }
        public int Op15 { get; set; }
        public int Op16 { get; set; }
        public int Op17 { get; set; }
        public int Op18 { get; set; }
        public int Op19 { get; set; }
        public int Op20 { get; set; }
        public int Op21 { get; set; }
        public int Total = 0;

        public int OP1 { get; set; }
        public int OP2 { get; set; }
        public int OP3 { get; set; }
        public int OP4 { get; set; }
        public int OP5 { get; set; }
        public int OP6 { get; set; }
        public int OP7 { get; set; }
        public int OP8 { get; set; }
        public int OP9 { get; set; }
        public int OP10 { get; set; }
        public int OP11 { get; set; }
        public int OP12 { get; set; }
        public int OP13 { get; set; }
        public int OP14 { get; set; }
        public int OP15 { get; set; }
        public int OP16 { get; set; }
        public int OP17 { get; set; }
        public int OP18 { get; set; }
        public int OP19 { get; set; }
        public int OP20 { get; set; }
        public int OP21 { get; set; }
        public int Tot = 0;

        public int ObtenerResultadosA()
        {
            Total = Op1 + Op2+ Op3+ Op4+ Op5+ Op6+ Op7+ Op8+ Op9+ Op10+ Op11+ Op12+ Op13+ Op14+ Op15+ Op16+ Op17+ Op18+ Op19+ Op20+ Op21;
            return Total;
        }

        public int ObtenerResultadosD()
        {
            Tot = OP1 + OP2 + OP3 + OP4 + OP5 + OP6 + OP7 + OP8 + OP9 + OP10 + OP11 + OP12 + OP13 + OP14 + OP15 + OP16 + OP17 + OP18 + OP19 + OP20 + OP21;
            return Tot;
        }
    }
}
