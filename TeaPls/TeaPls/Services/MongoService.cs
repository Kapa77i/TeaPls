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
    public class MongoService
    {
        string dbName = "TeaTask";
        string collectionName = "TaskList";

        IMongoCollection<TeaAforism> aforismsCollection;

        public object APIKeys { get; private set; }

        IMongoCollection<TeaAforism> AforismsCollection
        {
            get
            {
                if (aforismsCollection == null)
                {
                    //APIKeys.Connection string is found in the portal under the "Connection String" blade
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl("mongodb://teapls-db:D9mAfQnf6RmE3ja7MQpIhnoCcstbT7kd2ISI0U1TOxH3p2cXxTl17al6sAcwdVWRAGCRDCnRnZ2RACDbWobcmw==@teapls-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@teapls-db@")
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

        public async Task<List<TeaAforism>> GetAllAforisms()
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

        public async Task<TeaAforism> GetAforismById(Guid aforismId)
        {
            var singleAforism = await AforismsCollection
                .Find(f => f.Id.Equals(aforismId))
                .FirstOrDefaultAsync();
            return singleAforism;
        }

        #endregion

        #region Search Functions
        public async Task<List<TeaAforism>> GetAforismsByName(String text)
        {
            var aforisms = await AforismsCollection
                .AsQueryable()
                .Where(t => t.Text == text)
                .ToListAsync();

            return aforisms;
        }

        #endregion

        #region Save/Delete Functions
        public async Task CreateTask(TeaAforism aforism)
        {
            await AforismsCollection.InsertOneAsync(aforism);
        }

        public async Task UpdateAforism(TeaAforism aforism)
        {
            await AforismsCollection.ReplaceOneAsync(t => t.Id.Equals(aforism.Id), aforism);
        }

        public async Task DeleteAforism(TeaAforism aforism)
        {
            await AforismsCollection.DeleteOneAsync(t => t.Id.Equals(aforism.Id));
        }


        #endregion

    }




}
