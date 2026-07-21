using System;
using System.Collections.Generic;
using Phonebook;

namespace PhonebookTests;

public class PhoneNumberValidatorTests
{
  [Test]
  public void Validate_ValidPhone_ShouldNotThrowException()
  {
    // Arrange
    var phone = new PhoneNumber("+7 (999) 123-4567", PhoneNumberType.Personal);

    //Act & Assert
    Assert.DoesNotThrow(() => PhoneNumberValidator.Validate(phone));
  }
  
  [Test]
  public void Validate_InvalidPhone_ShouldThrowException()
  {
    // Arrange
    var phone = new PhoneNumber("12345", PhoneNumberType.Personal);

    //Act & Assert
    Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(phone));
  }
  
  [Test]
  public void ValidateList_ValidList_ShouldNotThrowException()
  {
    // Arrange
    var phones = new List<PhoneNumber>
    {
      new("+7 (999) 111-1111", PhoneNumberType.Personal),
      new("+7 (999) 222-2222", PhoneNumberType.Work)
    };

    //Act & Assert
    Assert.DoesNotThrow(() => PhoneNumberValidator.ValidateList(phones));
  }

  [Test]
  public void ValidateList_InvalidList_ShouldThrowException()
  {
    // Arrange
    var phones = new List<PhoneNumber>
    {
      new("1111", PhoneNumberType.Personal)
    };

    //Act & Assert
    Assert.Throws<ArgumentException>(() => PhoneNumberValidator.ValidateList(phones));
  }
}