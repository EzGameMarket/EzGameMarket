using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageScaler.Abstractions
{
    public interface IImageScalerService
    {
        /// <summary>
        /// Az image paraméterben megadott képet a times paraméterben megadott constans értékkel skálázza fel
        /// Ha a times kisebb mint 0 akkor kicsinyíti, ha nagyobb akkor nagyítja, ha pedig pontossan 0 akkor nem változtat a képen
        /// </summary>
        /// <param name="image">A kép amit módosítani kell</param>
        /// <param name="times">A skála, hogy hányszorosra modósítja</param>
        /// <returns>Vissza adja a módosításokon átesett képet</returns>
        Task<byte[]> ScaleAsync(byte[] image, int times);
    }
}
