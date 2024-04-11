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
    describe('ContentCategoryContract', function() {
      beforeEach(function() {
        instance = new ParehNegarwebApi.ContentCategoryContract();
      });

      it('should create an instance of ContentCategoryContract', function() {
        // TODO: update the code to test ContentCategoryContract
        expect(instance).to.be.a(ParehNegarwebApi.ContentCategoryContract);
      });

      it('should have the property id (base name: "id")', function() {
        // TODO: update the code to test the property id
        expect(instance).to.have.property('id');
        // expect(instance.id).to.be(expectedValueLiteral);
      });

      it('should have the property creationDateTime (base name: "creationDateTime")', function() {
        // TODO: update the code to test the property creationDateTime
        expect(instance).to.have.property('creationDateTime');
        // expect(instance.creationDateTime).to.be(expectedValueLiteral);
      });

      it('should have the property modificationDateTime (base name: "modificationDateTime")', function() {
        // TODO: update the code to test the property modificationDateTime
        expect(instance).to.have.property('modificationDateTime');
        // expect(instance.modificationDateTime).to.be(expectedValueLiteral);
      });

      it('should have the property isDeleted (base name: "isDeleted")', function() {
        // TODO: update the code to test the property isDeleted
        expect(instance).to.have.property('isDeleted');
        // expect(instance.isDeleted).to.be(expectedValueLiteral);
      });

      it('should have the property deletedDateTime (base name: "deletedDateTime")', function() {
        // TODO: update the code to test the property deletedDateTime
        expect(instance).to.have.property('deletedDateTime');
        // expect(instance.deletedDateTime).to.be(expectedValueLiteral);
      });

      it('should have the property key (base name: "key")', function() {
        // TODO: update the code to test the property key
        expect(instance).to.have.property('key');
        // expect(instance.key).to.be(expectedValueLiteral);
      });

      it('should have the property contents (base name: "contents")', function() {
        // TODO: update the code to test the property contents
        expect(instance).to.have.property('contents');
        // expect(instance.contents).to.be(expectedValueLiteral);
      });

    });
  });

}));