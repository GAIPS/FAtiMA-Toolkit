using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal
{
    public class BaseDomainObject
    {
        public Guid Id { get; set; }

        public BaseDomainObject()
        {
            Id = Guid.NewGuid();
        }
    }
}
