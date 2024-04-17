# ParehNegarwebApi.ContentApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiContentAddBulkContentWithKeyPost**](ContentApi.md#apiContentAddBulkContentWithKeyPost) | **POST** /api/Content/AddBulkContentWithKey | 
[**apiContentAddContentWithKeyPost**](ContentApi.md#apiContentAddContentWithKeyPost) | **POST** /api/Content/AddContentWithKey | 
[**apiContentDeleteByKeyPost**](ContentApi.md#apiContentDeleteByKeyPost) | **POST** /api/Content/DeleteByKey | 
[**apiContentGetByLanguagePost**](ContentApi.md#apiContentGetByLanguagePost) | **POST** /api/Content/GetByLanguage | 

<a name="apiContentAddBulkContentWithKeyPost"></a>
# **apiContentAddBulkContentWithKeyPost**
> MessageContract apiContentAddBulkContentWithKeyPost(opts)



### Example
```javascript
import {ParehNegarwebApi} from 'pareh_negarweb_api';
let defaultClient = ParehNegarwebApi.ApiClient.instance;

// Configure API key authorization: Bearer
let Bearer = defaultClient.authentications['Bearer'];
Bearer.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//Bearer.apiKeyPrefix = 'Token';

let apiInstance = new ParehNegarwebApi.ContentApi();
let opts = { 
  'body': [new ParehNegarwebApi.AddContentWithKeyRequestContract()] // [AddContentWithKeyRequestContract] | 
};
apiInstance.apiContentAddBulkContentWithKeyPost(opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully. Returned data: ' + data);
  }
});
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**[AddContentWithKeyRequestContract]**](AddContentWithKeyRequestContract.md)|  | [optional] 

### Return type

[**MessageContract**](MessageContract.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

<a name="apiContentAddContentWithKeyPost"></a>
# **apiContentAddContentWithKeyPost**
> MessageContract apiContentAddContentWithKeyPost(opts)



### Example
```javascript
import {ParehNegarwebApi} from 'pareh_negarweb_api';
let defaultClient = ParehNegarwebApi.ApiClient.instance;

// Configure API key authorization: Bearer
let Bearer = defaultClient.authentications['Bearer'];
Bearer.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//Bearer.apiKeyPrefix = 'Token';

let apiInstance = new ParehNegarwebApi.ContentApi();
let opts = { 
  'body': new ParehNegarwebApi.AddContentWithKeyRequestContract() // AddContentWithKeyRequestContract | 
};
apiInstance.apiContentAddContentWithKeyPost(opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully. Returned data: ' + data);
  }
});
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**AddContentWithKeyRequestContract**](AddContentWithKeyRequestContract.md)|  | [optional] 

### Return type

[**MessageContract**](MessageContract.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

<a name="apiContentDeleteByKeyPost"></a>
# **apiContentDeleteByKeyPost**
> MessageContract apiContentDeleteByKeyPost(opts)



### Example
```javascript
import {ParehNegarwebApi} from 'pareh_negarweb_api';
let defaultClient = ParehNegarwebApi.ApiClient.instance;

// Configure API key authorization: Bearer
let Bearer = defaultClient.authentications['Bearer'];
Bearer.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//Bearer.apiKeyPrefix = 'Token';

let apiInstance = new ParehNegarwebApi.ContentApi();
let opts = { 
  'body': new ParehNegarwebApi.DeleteByKeyRequestContract() // DeleteByKeyRequestContract | 
};
apiInstance.apiContentDeleteByKeyPost(opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully. Returned data: ' + data);
  }
});
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DeleteByKeyRequestContract**](DeleteByKeyRequestContract.md)|  | [optional] 

### Return type

[**MessageContract**](MessageContract.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

<a name="apiContentGetByLanguagePost"></a>
# **apiContentGetByLanguagePost**
> ContentResponseContractMessageContract apiContentGetByLanguagePost(opts)



### Example
```javascript
import {ParehNegarwebApi} from 'pareh_negarweb_api';
let defaultClient = ParehNegarwebApi.ApiClient.instance;

// Configure API key authorization: Bearer
let Bearer = defaultClient.authentications['Bearer'];
Bearer.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//Bearer.apiKeyPrefix = 'Token';

let apiInstance = new ParehNegarwebApi.ContentApi();
let opts = { 
  'body': new ParehNegarwebApi.GetByLanguageRequestContract() // GetByLanguageRequestContract | 
};
apiInstance.apiContentGetByLanguagePost(opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully. Returned data: ' + data);
  }
});
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**GetByLanguageRequestContract**](GetByLanguageRequestContract.md)|  | [optional] 

### Return type

[**ContentResponseContractMessageContract**](ContentResponseContractMessageContract.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

