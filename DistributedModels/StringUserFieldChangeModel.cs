using System;

namespace DistributedModels
{
    public class StringUserFieldChangeModel
    {
        public Guid UserId { get; set; }
        public string FieldToUpdate { get; set; }
    }
}
