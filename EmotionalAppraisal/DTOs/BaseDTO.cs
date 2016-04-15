using System;

namespace EmotionalAppraisal.DTOs
{
    public class BaseDTO
    {
        public Guid Id { get; set; }

	    public BaseDTO()
	    {
			Id = Guid.NewGuid();
	    }
    }
}
