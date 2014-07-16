BinbinMessageQueue
==================

Message Queue redis mongo



MongoMessageQueue use mongodb://localhost/messagebus

RedisMessageBus use new RedisClient("localhost", 6379)
RedisMessageBus use redis pub/sub feature
连接断开后，所有的publishmessage会丢失，不能保存