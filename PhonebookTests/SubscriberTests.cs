using System;
using Phonebook;

namespace PhonebookTests;

public class SubscriberTests
{
  [Test]
  public void Equals_SameId_ShouldBeTrue()
  {
    // Arrange
    var id = Guid.NewGuid();
    var first = new Subscriber(id, "Ivan", new());
    var second = new Subscriber(id, "Alex", new());

    // Act & Assert
    Assert.That(first, Is.EqualTo(second));
  }

  [Test]
  public void Equals_DifferentId_ShouldBeFalse()
  {
    // Arrange
    var first = new Subscriber("Ivan", new());
    var second = new Subscriber("Alex", new());

    // Act & Assert
    Assert.That(first, Is.Not.EqualTo(second));
  }
}