package momento.client.example;

import momento.sdk.SimpleCacheClient;
import momento.sdk.exceptions.AlreadyExistsException;
import momento.sdk.messages.CacheGetResponse;

public class MomentoCacheApplication {

  private static final String MOMENTO_AUTH_TOKEN = System.getenv("MOMENTO_AUTH_TOKEN");
  private static final String CACHE_NAME = "cache";
  private static final String KEY = "key";
  private static final String VALUE = "value";
  private static final int DEFAULT_ITEM_TTL_SECONDS = 60;

  public static void main(String[] args) {
    printStartBanner();
    try (SimpleCacheClient simpleCacheClient =
        SimpleCacheClient.builder(MOMENTO_AUTH_TOKEN, DEFAULT_ITEM_TTL_SECONDS).build()) {

      createCache(simpleCacheClient, CACHE_NAME);

      System.out.println(String.format("Setting key=`%s` , value=`%s`", KEY, VALUE));
      simpleCacheClient.set(CACHE_NAME, KEY, VALUE);

      System.out.println(String.format("Getting value for key=`%s`", KEY));

      CacheGetResponse getResponse = simpleCacheClient.get(CACHE_NAME, KEY);
      System.out.println(String.format("Lookup resulted in: `%s`", getResponse.status()));
      System.out.println(
          String.format("Looked up value=`%s`", getResponse.string().orElse("NOT FOUND")));
    }
    printEndBanner();
  }

  private static void createCache(SimpleCacheClient simpleCacheClient, String cacheName) {
    try {
      simpleCacheClient.createCache(cacheName);
    } catch (AlreadyExistsException e) {
      System.out.println(String.format("Cache with name `%s` already exists.", cacheName));
    }
  }

  private static void printStartBanner() {
    System.out.println("******************************************************************");
    System.out.println("*                      Momento Example Start                     *");
    System.out.println("******************************************************************");
  }

  private static void printEndBanner() {
    System.out.println("******************************************************************");
    System.out.println("*                       Momento Example End                      *");
    System.out.println("******************************************************************");
  }
}
