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
(function(root, factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD.
    define(['expect.js', '../../src/index'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    factory(require('expect.js'), require('../../src/index'));
  } else {
    // Browser globals (root is window)
    factory(root.expect, root.ParehNegarwebApi);
  }
}(this, function(expect, ParehNegarwebApi) {
  'use strict';

  var instance;

  describe('(package)', function() {
    describe('ContentCategoryContractListMessageContract', function() {
      beforeEach(function() {
        instance = new ParehNegarwebApi.ContentCategoryContractListMessageContract();
      });

      it('should create an instance of ContentCategoryContractListMessageContract', function() {
        // TODO: update the code to test ContentCategoryContractListMessageContract
        expect(instance).to.be.a(ParehNegarwebApi.ContentCategoryContractListMessageContract);
      });

      it('should have the property isSuccess (base name: "isSuccess")', function() {
        // TODO: update the code to test the property isSuccess
        expect(instance).to.have.property('isSuccess');
        // expect(instance.isSuccess).to.be(expectedValueLiteral);
      });

      it('should have the property error (base name: "error")', function() {
        // TODO: update the code to test the property error
        expect(instance).to.have.property('error');
        // expect(instance.error).to.be(expectedValueLiteral);
      });

      it('should have the property success (base name: "success")', function() {
        // TODO: update the code to test the property success
        expect(instance).to.have.property('success');
        // expect(instance.success).to.be(expectedValueLiteral);
      });

      it('should have the property result (base name: "result")', function() {
        // TODO: update the code to test the property result
        expect(instance).to.have.property('result');
        // expect(instance.result).to.be(expectedValueLiteral);
      });

      it('should have the property totalCount (base name: "totalCount")', function() {
        // TODO: update the code to test the property totalCount
        expect(instance).to.have.property('totalCount');
        // expect(instance.totalCount).to.be(expectedValueLiteral);
      });

      it('should have the property hasItems (base name: "hasItems")', function() {
        // TODO: update the code to test the property hasItems
        expect(instance).to.have.property('hasItems');
        // expect(instance.hasItems).to.be(expectedValueLiteral);
      });

    });
  });

}));
