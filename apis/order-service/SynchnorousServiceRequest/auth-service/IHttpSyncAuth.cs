namespace order_service.SynchnorousServiceRequest.auth_service;

public interface IHttpSyncAuth
{
    //check user exist
    Task<bool> CheckUserExist(int userId);
}