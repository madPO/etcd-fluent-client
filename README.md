# etcd-fluent-client
Full example in test cases.
## Usage
### Create client
Create _EtcdClient_ and add _EtcdGrpcTransport_ for Etcdv3. And use _RoundRobinGateway_, for round robin connection.    
**Example**:    
```c#
  var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));
  using(clinet)
  {
    // ....
  }
```
### Write a key
Put key value pair, where key - string, and value - byte[].    
**Example**:
```c#
 await client.PutAsync(key, value);
```
Add lease, where lease is _EtcdLease_ class, **example**:
```c#
await client.Put(key, value)
            .WithLeaseAsync(lease);
```
### Read keys
**Example**:
Key - string, return type is _ReadOnlyCollection<byte[]>_.
```c#
var result = await client.GetAsync(key);
```
Range from _key1_ to _key2_, **example**:
```c#
var result = await client.Get(firstKey)
                    .ToKey(lastKey)
                    .ExecuteAsync();
```     
Search values by prefix - not implemented.     
Get values with limit - not implemented.    
### Read past version of keys
Not implemented.     
### Read keys which are greater than or equal to the byte value of the specified key    
Not implemented.
### Delete keys
Where key - string.        
**Example**:
```c#
await client.DeleteAsync(key);
```
Range delete, **example**:
```c#
 await client.Delete(firtsKey)
             .ToKey(lastKey)
             .ExecuteAsync();
```    
Prefix delete - not implemented.    
### Watch key changes
Not implemented.    
### Compacted revisions
Not implemented.    
### Grant leases
Where ttl - long (seconds).    
**Example**:
```c#
var lease = await client.GrantLeaseAsync(ttl);
```
### Revoke leases
Not implemented.     
### Keep leases alive
Not implemented.    
### Get lease information
Not implemented.    
## Authentication
Not implemented.
## Server selection
Option implemented - _RoundRobinGateway_.
**Example**:
```c#
  var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));
  using(clinet)
  {
    // ....
  }
```