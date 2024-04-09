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
    describe('ServiceDetailsContract', function() {
      beforeEach(function() {
        instance = new ParehNegarwebApi.ServiceDetailsContract();
      });

      it('should create an instance of ServiceDetailsContract', function() {
        // TODO: update the code to test ServiceDetailsContract
        expect(instance).to.be.a(ParehNegarwebApi.ServiceDetailsContract);
      });

      it('should have the property serviceRouteAddress (base name: "serviceRouteAddress")', function() {
        // TODO: update the code to test the property serviceRouteAddress
        expect(instance).to.have.property('serviceRouteAddress');
        // expect(instance.serviceRouteAddress).to.be(expectedValueLiteral);
      });

      it('should have the property methodName (base name: "methodName")', function() {
        // TODO: update the code to test the property methodName
        expect(instance).to.have.property('methodName');
        // expect(instance.methodName).to.be(expectedValueLiteral);
      });

      it('should have the property path (base name: "path")', function() {
        // TODO: update the code to test the property path
        expect(instance).to.have.property('path');
        // expect(instance.path).to.be(expectedValueLiteral);
      });

      it('should have the property projectName (base name: "projectName")', function() {
        // TODO: update the code to test the property projectName
        expect(instance).to.have.property('projectName');
        // expect(instance.projectName).to.be(expectedValueLiteral);
      });

    });
  });

}));
