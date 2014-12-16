rvalidate
=========

Validation attributes for WebAPI, that cares about request method

  DataAnnotation attributes is really cool, but when you using WebApi you need way 
to validate same view models different ways when performing POST, GET, DELETE.
Sometime you solve this problem by hands validation. Sometime- by using dynamic 
automapping. 
Now you can solve problem or get new one with this library. Just specify http method
in attribute ctor on which you want to validate model, put RValidate attr on controller 
and disable standart model validation.

**PM> Install-Package RValidate**

Three simple steps:
==================

1. Add attributes to your model
```csharp
public class SimpleModel {
  [RRequired]
  [DisplayName("MySuperKey")]
  public int Key { get;set;}
  [RPassword(RPasswordStrength.VeryStrong, RHttpMethods.Post)]
  public string Password { get;set; }
}
```
2. Add attribute to API controller or controller method.
```csharp
[RValidate]
public class MyController: ApiController
{
  ...
}
```
3. Remove standart model validation service from config:
```csharp
public static class WebApiConfig {
  public static void Register(HttpConfiguration config)
  {
    config.Services.Clear(typeof(ModelValidatorProvider));
    ....
  }
}
```

Thats all. It works like DataAnnotation attributes.

Attributes
==========

**RCompareAttribute** - Compares property with another one. Less, More, Equal

**RCreditCardAttribute** - Validate that string projection of property is a valid mod10 credit card

**RDateInRangeAttribute** - Validate, that property in date range

**RDateTimeAttribute** - Validate, that stringified projection of property can be parsed as DateTime type

**REmailAddressAttribute** -  Validate, that stringified projection of property is valid email address

**RMaxLengthAttribute** - Validate, that property have length less or equal defined. Property that verified, should be castable                       to IEnumerable interface (strings, collections)

**RMinLengthAttribute** - Validate, that property have length more or equal defined. Property that verified, should be castable                       to IEnumerable interface (strings, collections)

**RPasswordAttribute** - Validates that property projection to string have valid password strength

**RRangeAttribute** -  Validate, that property projection to string and converted to numeric in specified range

**RRegularExpressionAttribute** - Validates, that proerty string projection match pattern

**RRequiredAttribute** -  Validates, that property have no default value of property type

**RRequiredEnumerableAttribute** - Validates, that collection should have at least one member and not be null

**RUrlAttribute** - Validates, that string projection of property is valid URL


License
=======
Copyright (c) Uladzimir Harabtsou

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
