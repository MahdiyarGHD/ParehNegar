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

/**
 * The ValidationContract model module.
 * @module model/ValidationContract
 * @version 1.0
 */
export default class ValidationContract {
  /**
   * Constructs a new <code>ValidationContract</code>.
   * @alias module:model/ValidationContract
   * @class
   */
  constructor() {
  }

  /**
   * Constructs a <code>ValidationContract</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/ValidationContract} obj Optional instance to populate.
   * @return {module:model/ValidationContract} The populated <code>ValidationContract</code> instance.
   */
  static constructFromObject(data, obj) {
    if (data) {
      obj = obj || new ValidationContract();
      if (data.hasOwnProperty('message'))
        obj.message = ApiClient.convertToType(data['message'], 'String');
    }
    return obj;
  }
}

/**
 * @member {String} message
 */
ValidationContract.prototype.message = undefined;

