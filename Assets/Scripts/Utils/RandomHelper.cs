using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelper {
  static public T RandomChoose<T>(T[] array, T[] excludes = null) {
    if (excludes != null) {
      List<T> list = new List<T>(array);
      foreach(T v in excludes) {
        list.Remove(v);
      }
      array = list.ToArray();
    }
    int choice = Random.Range(0, array.Length);
    return array[choice];
  }
}