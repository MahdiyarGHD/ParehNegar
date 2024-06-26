/*
 * ParehNegar.WebApi
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 *
 * Swagger Codegen version: 3.0.54
 *
 * Do not edit the class manually.
 *
 */
import ApiClient from './ApiClient';
import AddContentWithKeyRequestContract from './model/AddContentWithKeyRequestContract';
import ContentCategoryContract from './model/ContentCategoryContract';
import ContentCategoryContractListMessageContract from './model/ContentCategoryContractListMessageContract';
import ContentCategoryResponseContract from './model/ContentCategoryResponseContract';
import ContentContract from './model/ContentContract';
import ContentResponseContract from './model/ContentResponseContract';
import ContentResponseContractMessageContract from './model/ContentResponseContractMessageContract';
import DeleteByKeyRequestContract from './model/DeleteByKeyRequestContract';
import ErrorContract from './model/ErrorContract';
import FailedReasonType from './model/FailedReasonType';
import GetByLanguageRequestContract from './model/GetByLanguageRequestContract';
import LanguageContract from './model/LanguageContract';
import LanguageDataContract from './model/LanguageDataContract';
import LanguageResponseContract from './model/LanguageResponseContract';
import MessageContract from './model/MessageContract';
import ServiceDetailsContract from './model/ServiceDetailsContract';
import SuccessContract from './model/SuccessContract';
import ValidationContract from './model/ValidationContract';
import ContentApi from './api/ContentApi';
import HomeApi from './api/HomeApi';

/**
* Object.<br>
* The <code>index</code> module provides access to constructors for all the classes which comprise the public API.
* <p>
* An AMD (recommended!) or CommonJS application will generally do something equivalent to the following:
* <pre>
* var ParehNegarwebApi = require('index'); // See note below*.
* var xxxSvc = new ParehNegarwebApi.XxxApi(); // Allocate the API class we're going to use.
* var yyyModel = new ParehNegarwebApi.Yyy(); // Construct a model instance.
* yyyModel.someProperty = 'someValue';
* ...
* var zzz = xxxSvc.doSomething(yyyModel); // Invoke the service.
* ...
* </pre>
* <em>*NOTE: For a top-level AMD script, use require(['index'], function(){...})
* and put the application logic within the callback function.</em>
* </p>
* <p>
* A non-AMD browser application (discouraged) might do something like this:
* <pre>
* var xxxSvc = new ParehNegarwebApi.XxxApi(); // Allocate the API class we're going to use.
* var yyy = new ParehNegarwebApi.Yyy(); // Construct a model instance.
* yyyModel.someProperty = 'someValue';
* ...
* var zzz = xxxSvc.doSomething(yyyModel); // Invoke the service.
* ...
* </pre>
* </p>
* @module index
* @version 1.0
*/
export {
    /**
     * The ApiClient constructor.
     * @property {module:ApiClient}
     */
    ApiClient,

    /**
     * The AddContentWithKeyRequestContract model constructor.
     * @property {module:model/AddContentWithKeyRequestContract}
     */
    AddContentWithKeyRequestContract,

    /**
     * The ContentCategoryContract model constructor.
     * @property {module:model/ContentCategoryContract}
     */
    ContentCategoryContract,

    /**
     * The ContentCategoryContractListMessageContract model constructor.
     * @property {module:model/ContentCategoryContractListMessageContract}
     */
    ContentCategoryContractListMessageContract,

    /**
     * The ContentCategoryResponseContract model constructor.
     * @property {module:model/ContentCategoryResponseContract}
     */
    ContentCategoryResponseContract,

    /**
     * The ContentContract model constructor.
     * @property {module:model/ContentContract}
     */
    ContentContract,

    /**
     * The ContentResponseContract model constructor.
     * @property {module:model/ContentResponseContract}
     */
    ContentResponseContract,

    /**
     * The ContentResponseContractMessageContract model constructor.
     * @property {module:model/ContentResponseContractMessageContract}
     */
    ContentResponseContractMessageContract,

    /**
     * The DeleteByKeyRequestContract model constructor.
     * @property {module:model/DeleteByKeyRequestContract}
     */
    DeleteByKeyRequestContract,

    /**
     * The ErrorContract model constructor.
     * @property {module:model/ErrorContract}
     */
    ErrorContract,

    /**
     * The FailedReasonType model constructor.
     * @property {module:model/FailedReasonType}
     */
    FailedReasonType,

    /**
     * The GetByLanguageRequestContract model constructor.
     * @property {module:model/GetByLanguageRequestContract}
     */
    GetByLanguageRequestContract,

    /**
     * The LanguageContract model constructor.
     * @property {module:model/LanguageContract}
     */
    LanguageContract,

    /**
     * The LanguageDataContract model constructor.
     * @property {module:model/LanguageDataContract}
     */
    LanguageDataContract,

    /**
     * The LanguageResponseContract model constructor.
     * @property {module:model/LanguageResponseContract}
     */
    LanguageResponseContract,

    /**
     * The MessageContract model constructor.
     * @property {module:model/MessageContract}
     */
    MessageContract,

    /**
     * The ServiceDetailsContract model constructor.
     * @property {module:model/ServiceDetailsContract}
     */
    ServiceDetailsContract,

    /**
     * The SuccessContract model constructor.
     * @property {module:model/SuccessContract}
     */
    SuccessContract,

    /**
     * The ValidationContract model constructor.
     * @property {module:model/ValidationContract}
     */
    ValidationContract,

    /**
    * The ContentApi service constructor.
    * @property {module:api/ContentApi}
    */
    ContentApi,

    /**
    * The HomeApi service constructor.
    * @property {module:api/HomeApi}
    */
    HomeApi
};
