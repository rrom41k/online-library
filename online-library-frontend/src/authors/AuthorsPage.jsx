import React, { Component } from "react";
import BookCarousel from "../books/BookCarousel";

export default class AuthorsPage extends Component {
  constructor(props) {
    super(props);

    this.state = {
      error: null,
      isLoaded: false,
      url: "http://localhost:7140/api/authors/",
      authors: [],
    };
  }

  componentDidMount() {
    this.getAuthors();
  }

  getAuthors() {
    fetch(this.state.url)
      .then((response) => response.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            authors: result,
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

  createFIO(author) {
    return `${author["name"]} ${author["secondName"]} ${author["surname"]}`;
  }

  render() {
    const { error, isLoaded, url, authors } = this.state;
    const style = {
      marginLeft: "20%",
      marginRight: "20%",
    };
    if (!error) {
      if (isLoaded) {
        return (
          <div style={style}>
            {authors.map((author) => {
              return (
                <BookCarousel
                  title={this.createFIO(author)}
                  titleUrl={"/authors/" + author["slug"]}
                  collectionUrl={url + author["slug"]}
                  maxCardsCount={5}
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
