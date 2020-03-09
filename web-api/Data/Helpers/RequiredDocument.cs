using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;

namespace DEDrake.Data.Helpers {
  public class MongoRequiredProperties {

    public static bool IsValid<T>(T item) {
      var valid = true;

      var props = item.GetType().GetProperties();

      foreach (var prop in props) {
        var attr = prop.GetCustomAttribute(typeof(BsonRequiredAttribute));
        if (attr != null) {
          var val = prop.GetValue(item);
          if (val == null) {
            valid = false;
            break;
          }
        }

      }

      return valid;
    }
  }
}
