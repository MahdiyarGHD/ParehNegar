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
import ApiClient from '../ApiClient';
import ContentResponseContract from './ContentResponseContract';
import ErrorContract from './ErrorContract';
import SuccessContract from './SuccessContract';

/**
 * The ContentResponseContractMessageContract model module.
 * @module model/ContentResponseContractMessageContract
 * @version 1.0
 */
export default class ContentResponseContractMessageContract {
  /**
   * Constructs a new <code>ContentResponseContractMessageContract</code>.
   * @alias module:model/ContentResponseContractMessageContract
   * @class
   */
  constructor() {
  }

  /**
   * Constructs a <code>ContentResponseContractMessageContract</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/ContentResponseContractMessageContract} obj Optional instance to populate.
   * @return {module:model/ContentResponseContractMessageContract} The populated <code>ContentResponseContractMessageContract</code> instance.
   */
  static constructFromObject(data, obj) {
    if (data) {
      obj = obj || new ContentResponseContractMessageContract();
      if (data.hasOwnProperty('isSuccess'))
        obj.isSuccess = ApiClient.convertToType(data['isSuccess'], 'Boolean');
      if (data.hasOwnProperty('error'))
        obj.error = ErrorContract.constructFromObject(data['error']);
      if (data.hasOwnProperty('success'))
        obj.success = SuccessContract.constructFromObject(data['success']);
      if (data.hasOwnProperty('result'))
        obj.result = ContentResponseContract.constructFromObject(data['result']);
    }
    return obj;
  }
}

/**
 * @member {Boolean} isSuccess
 */
ContentResponseContractMessageContract.prototype.isSuccess = undefined;

/**
 * @member {module:model/ErrorContract} error
 */
ContentResponseContractMessageContract.prototype.error = undefined;

/**
 * @member {module:model/SuccessContract} success
 */
ContentResponseContractMessageContract.prototype.success = undefined;

/**
 * @member {module:model/ContentResponseContract} result
 */
ContentResponseContractMessageContract.prototype.result = undefined;

