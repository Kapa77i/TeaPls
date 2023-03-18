using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TeaPls.Models;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Diagnostics;
using MongoDB.Driver.Linq;

namespace TeaPls.Services
{
    public static class MongoService
    {
       static string dbName = "teapls";
        static string collectionName = "teaaforism";

        static IMongoCollection<TeaAforism> aforismsCollection;

        static IMongoCollection<TeaAforism> AforismsCollection
        {
            get
            {
                if (aforismsCollection == null)
                {
                    string connectionString =
  @"mongodb://teapls-db:D9mAfQnf6RmE3ja7MQpIhnoCcstbT7kd2ISI0U1TOxH3p2cXxTl17al6sAcwdVWRAGCRDCnRnZ2RACDbWobcmw==@teapls-db.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@teapls-db@";
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    //Clientti
                    var mongoClient = new MongoClient(settings);

                    //Luo tai hakee DB:n
                    var db = mongoClient.GetDatabase(dbName);

                    //Luo tai hakee collectionin
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    aforismsCollection = db.GetCollection<TeaAforism>(collectionName, collectionSettings);

                }

                return aforismsCollection;

            }

        }


        #region Get Functions

        public async static Task<List<TeaAforism>> GetAllAforisms()
        {
            try
            {
                var allAforisms = await AforismsCollection
                    .Find(new BsonDocument())
                    .ToListAsync();

                return allAforisms;

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
            return null;

        }

        public async static Task<TeaAforism> GetAforismById(Guid aforismId)
        {
            var singleAforism = await AforismsCollection
                .Find(f => f._id.Equals(aforismId))
                .FirstOrDefaultAsync();
            return singleAforism;
        }

        #endregion

        #region Search Functions
        public async static Task<List<TeaAforism>> GetAforismsByName(String text)
        {
            var aforisms = await AforismsCollection
                .AsQueryable()
                .Where(t => t.Text == text)
                .ToListAsync();

            return aforisms;
        }

        #endregion

        #region Save/Delete Functions
        public async static Task CreateAforism(TeaAforism aforism)
        {
            await AforismsCollection.InsertOneAsync(aforism);
        }

        public static async Task CreateAf(string text, string description)
        {
        
            var aforism = new TeaAforism
            {
                _id = Guid.NewGuid().ToString(),
                Text = text,
                Description = description
            };
            
            await AforismsCollection.InsertOneAsync(aforism);
        }

        public async static Task UpdateAforism(TeaAforism aforism)
        {
            await AforismsCollection.ReplaceOneAsync(t => t._id.Equals(aforism._id), aforism);
        }

        public async static Task DeleteAforism(TeaAforism aforism)
        {
            await AforismsCollection.DeleteOneAsync(t => t._id.Equals(aforism._id));
        }


        #endregion

    }

}
