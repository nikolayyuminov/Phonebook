using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook;

namespace PhonebookTests;

public class PhonebookTests
{
  private Phonebook.Phonebook _phonebook;
  
  [SetUp]
  public void Setup()
  {
    _phonebook = new Phonebook.Phonebook(new List<Subscriber>());
  }

  [Test]
  public void AddSubscriber_ExistingSubscriber_ShouldThrowException()
  {
    // Arrange
    var subscriber = new Subscriber("Ivan", new());
    _phonebook.AddSubscriber(subscriber);

    // Act & Assert
    var actualSubscriber = _phonebook.GetSubscriber(subscriber.Id);

    Assert.Throws<InvalidOperationException>(() => _phonebook.AddSubscriber(actualSubscriber));
  }

  [Test]
  public void AddSubscriber_NewSubscriber_ShouldAddSubscriber()
  {
    // Arrange
    var subscriber =
      new Subscriber("Lisa", [new("+1(999)945-6321", PhoneNumberType.Personal)]);
    
    // Act 
    _phonebook.AddSubscriber(subscriber);
    
    // Assert
    var actualSubscriber = _phonebook.GetSubscriber(subscriber.Id);
    Assert.That(actualSubscriber, Is.EqualTo(subscriber));
  }

  [Test]
  public void GetSubscriber_ExistingSubscriber_ShouldReturnSubscriber()
  {
    // Arrange
    var subscriber = new Subscriber("Alex", [new("+7(999)945-6321", PhoneNumberType.Personal)]);
    _phonebook.AddSubscriber(subscriber);
    // Act
    var result = _phonebook.GetSubscriber(subscriber.Id);
    // Assert
    Assert.That(result, Is.EqualTo(subscriber));
  }
  
  [Test]
  public void GetSubscriber_NotFound_ShouldThrowException()
  {
    Assert.Throws<InvalidOperationException>(() =>
      _phonebook.GetSubscriber(Guid.NewGuid()));
  }
  
  [Test]
  public void GetAll_ShouldReturnAllSubscribers()
  {
    // Arrange
    var first = new Subscriber("Xpen", new());
    var second = new Subscriber("Xiaomi", new());

    _phonebook.AddSubscriber(first);
    _phonebook.AddSubscriber(second);
    // Act
    var allSubscribers = _phonebook.GetAll();
    
    // Assert
    Assert.That(allSubscribers.Count(), Is.EqualTo(2));
  }
  
  [Test]
  public void RenameSubscriber_ExistingSubscriber_ShouldChangeName()
  {
    // Arrange
    var subscriber = new Subscriber("Anatol", []);
    _phonebook.AddSubscriber(subscriber);
    
    // Act
    var newName = "Petrol";
    _phonebook.RenameSubscriber(subscriber, newName);

    //Assert
    var result = _phonebook.GetSubscriber(subscriber.Id);
    Assert.That(result.Name, Is.EqualTo(newName));
  }
  
  [Test]
  public void DeleteSubscriber_ExistingSubscriber_ShouldThrowException()
  {
    // Arrange
    var subscriber = new Subscriber("Ivan", []);
    _phonebook.AddSubscriber(subscriber);
    
    // Act
    _phonebook.DeleteSubscriber(subscriber);

    // Assert
    Assert.Throws<InvalidOperationException>(() => _phonebook.GetSubscriber(subscriber.Id));
  }
  
  [Test]
  public void DeleteSubscriber_NotExisting_ShouldDoNothing()
  {
    // Arrange
    var subscriber = new Subscriber("Loli", []);

    // Assert
    Assert.DoesNotThrow(() => _phonebook.DeleteSubscriber(subscriber));
  }
  
  [Test]
  public void UpdateSubscriber_NotExisting_ShouldThrow()
  {
    // Arrange
    var oldSubscriber = new Subscriber("Chelsy", [new PhoneNumber("+79999456321", PhoneNumberType.Personal)]);
    var newSubscriber = new Subscriber(oldSubscriber.Id, "Alex", [new PhoneNumber("+79999499321", PhoneNumberType.Work)]);
    
    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => _phonebook.UpdateSubscriber(oldSubscriber, newSubscriber));
  }
  
  [Test]
  public void AddNumberToSubscriber_ExistingSubscriber_ShouldAddPhoneNumber()
  {
    // Arrange
    var subscriber = new Subscriber("Ivan", new());
    _phonebook.AddSubscriber(subscriber);
    var phone = new PhoneNumber("+7 (999) 111-2233", PhoneNumberType.Personal);

    // Act
    _phonebook.AddNumberToSubscriber(subscriber, phone);
    
    // Assert
    var updated = _phonebook.GetSubscriber(subscriber.Id);
    Assert.That(updated.PhoneNumbers, Does.Contain(phone));
  }
  
  [Test]
  public void AddSecondPhoneNumber_ExistingSubscriber_ShouldContainTwoNumbers()
  {
    // Arrange
    var subscriber = new Subscriber("Ivan", [new PhoneNumber("+7 (999) 111-1111", PhoneNumberType.Personal)]);
    _phonebook.AddSubscriber(subscriber);

    // Act
    _phonebook.AddNumberToSubscriber(subscriber, new PhoneNumber("+7 (999) 222-2222", PhoneNumberType.Work));

    // Assert
    var updated = _phonebook.GetSubscriber(subscriber.Id);
    Assert.That(updated.PhoneNumbers, Has.Count.EqualTo(2));
  }
}