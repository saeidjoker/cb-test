using System.Collections.Generic;

namespace Cleverbit.CodingTask.Application.Models.Shared {

    public class Page<T> {
        public List<T> Items { get; set; }

        public long Total { get; set; }
    }

}