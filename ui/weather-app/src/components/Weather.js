import "./Weather.css";
import React, { useState, useEffect } from "react";
export default function WeatherComponent() {
  const weatherD = {
    name: "London",
    main: {
      temp: 10,
      humidity: 50,
    },
    weather: [
      {
        description: "Cloudy",
        icon: "04d",
      },
    ],
    wind: {
      speed: 10,
    },
  };

  const [weather, setWeather] = useState(weatherD);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(success, () => {
        setError(true);
        setLoading(false);
      });
    } else {
      console.log("Geolocation not supported");
    }
  }, []);

  function success(position) {
    setLoading(true);
    const latitude = position.coords.latitude;
    const longitude = position.coords.longitude;
    console.log(`Latitude: ${latitude}, Longitude: ${longitude}`);

    // // Make API call to OpenWeatherMap
    // fetch(
    //   `https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=b1bca86efa3b8c46d0aedf824be9b110&units=metric`
    // )
    //   .then((response) => response.json())
    //   .then((data) => {
    //     setWeather(data);
    //     console.log(data);
    //   })
    //   .catch((error) => console.log(error));
    setWeather(weatherD);
    setLoading(false);
  }

  function renderWeather() {
    return (
      <section className="layout">
        <div>
          <div className="card">
            <div className="card-item">Location: {weather?.name ?? "N/A"}</div>
            <div className="card-item">
              Wind: {weather?.wind?.speed ?? "N/A"} m/s
            </div>
            <div className="card-item">
              Weather: {weather?.weather[0]?.description ?? "N/A"}
            </div>
            <div className="card-item">
              Humidity: {weather?.main?.humidity ?? "N/A"} %
            </div>
          </div>
        </div>
        <div>
          <div className="card">
            <div className="card-item">
              <h1 className="temp">{weather?.main?.temp ?? "N/A"} °C</h1>
            </div>
            <div className="card-item">
              <img
                className="icon"
                src={
                  weather?.weather?.length === 1 &&
                  weather?.weather[0]?.icon != null &&
                  `http://openweathermap.org/img/w/${weather.weather[0].icon}.png`
                }
                alt="weather icon"
              />
            </div>
          </div>
        </div>
      </section>
    );
  }

  function renderLoading() {
    return (
      <div className="layout">
        <div className="lds-facebook">
          <div></div>
          <div></div>
          <div></div>
        </div>
      </div>
    );
  }

  function renderError() {
    return (
      <div className="layout" style={{ color: "red" }}>
        Unable to retrieve weather data
      </div>
    );
  }

  function renderMain() {
    if (loading) {
      return renderLoading();
    } else if (error) {
      return renderError();
    } else {
      return renderWeather();
    }
  }

  return <div>{renderMain()}</div>;
}
