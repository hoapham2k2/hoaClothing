namespace order_service.SynchnorousServiceRequest.product_service;

public interface IHttpSyncProduct
{
    Task<string> GetTest();
    
    //check product exist
    Task<bool> CheckProductExist(int productId);
}