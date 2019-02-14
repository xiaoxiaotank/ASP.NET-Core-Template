using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebApi.Core.Dtos
{
    public class PagedDto<TDto> : DtoBase where TDto : DtoBase
    {
        /// <summary>
        /// 每页默认显示最大数据数
        /// </summary>
        public const int DefaultSize = 20;

        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<TDto> Data { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页最多显示数据数量
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 数据总个数
        /// </summary>
        public int Total { get; set; }
    }
}
