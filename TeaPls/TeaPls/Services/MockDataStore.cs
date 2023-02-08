using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaPls.Models;

namespace TeaPls.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Lipton Earl grey", Description="Maustettu musta tee bergamottiaromilla. Hienovaraisen voimakas ja aromirikas bergamotilla maustettu musta tee." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Nordqvist Tiikerin päiväuni", Description="Herkullinen tiikeriklassikko jo vuosien takaa. Pehmeää hunajaa sekä raikkaita mustaseljan ja kvittenin aromeja. " },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Clipper Sitruuna-inkivääri", Description="Virkistävä ja kirpeä sitruunainen tee, jossa mukana potkua antamassa inkivääriä. Mukana teessä tujaus makua antavaa lakritsinjuurta, joka avittaa mm. ruuansulatusta." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Clipper Vihreä chai", Description="Kevyt ja virkistävä vihreä tee huolellisesti sekoitettuna perinteisen Intialaisen Chai teen ainesosiin: lämmittävä kaneli, kardemumma, sekä neilikka." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Forsman Luomu appelsiini", Description="Mustan Appelsiiniteen makuelämyksen takana ovat appelsiinin kuoripalat ja appelsiiniöljy. Aistikas tuoksu korostuu erityisesti eteerisen appelsiiniöljyn kautta." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Forsman Russia samovar blend", Description="Samovar Blend mustan teen sekoitukseen kuuluu hedelmäinen sekoitus ananas-, papaija- ja appelsiinin paloja. Tämän lisäksi tämä lievästi kofeiinipitoinen musta teemme sisältää saflorin kukkia ja tietenkin kvitteniä, jotka mahdollistavat tämän teeseoksen makunystyröitä hellästi hivelevän kokonaisuuden. " }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}