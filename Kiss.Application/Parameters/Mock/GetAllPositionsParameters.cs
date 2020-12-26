using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Application.Parameters.Mock
{
    public class GetAllPositionsParameters : QueryStringParameters
    {
        public string OrgCode { get; set; }
    }
}
