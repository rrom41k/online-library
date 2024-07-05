import React, { Component } from "react";
import BookCarousel from "./books/BookCarousel";

export default class GenresPage extends Component {
  constructor(props) {
    super(props);

    this.state = {
      error: null,
      isLoaded: false,
      url: "http://localhost:7140/api/genres/",
      genres: [],
    };
  }

  componentDidMount() {
    this.getGenres();
  }

  getGenres() {
    fetch(this.state.url)
      .then((response) => response.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            genres: result,
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  }

  render() {
    const { error, isLoaded, url, genres } = this.state;
    const style = {
      marginLeft: "20%",
      marginRight: "20%",
    };
    if (!error) {
      if (isLoaded) {
        return (
          <div style={style}>
            {genres.map((genre) => {
              return (
                <BookCarousel
                  title={genre["name"]}
                  titleUrl={"books?searchTerm=" + genre["name"]}
                  collectionUrl={url + genre["slug"]}
                  maxCardsCount={6}
                />
              );
            })}
          </div>
        );
      } else {
        return <div>Loading...</div>;
      }
    } else {
      <div>
        Error: {this.state.error.code} {this.state.error.message}
      </div>;
    }
  }
}
