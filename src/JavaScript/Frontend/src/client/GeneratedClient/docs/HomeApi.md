# ParehNegarwebApi.HomeApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiHomeGetAllContentsGet**](HomeApi.md#apiHomeGetAllContentsGet) | **GET** /api/Home/GetAllContents | 

<a name="apiHomeGetAllContentsGet"></a>
# **apiHomeGetAllContentsGet**
> ContentCategoryContractListMessageContract apiHomeGetAllContentsGet()



### Example
```javascript
import {ParehNegarwebApi} from 'pareh_negarweb_api';
let defaultClient = ParehNegarwebApi.ApiClient.instance;

// Configure API key authorization: Bearer
let Bearer = defaultClient.authentications['Bearer'];
Bearer.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//Bearer.apiKeyPrefix = 'Token';

let apiInstance = new ParehNegarwebApi.HomeApi();
apiInstance.apiHomeGetAllContentsGet((error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully. Returned data: ' + data);
  }
});
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**ContentCategoryContractListMessageContract**](ContentCategoryContractListMessageContract.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

