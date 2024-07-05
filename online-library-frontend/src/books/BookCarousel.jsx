import "../css/BookCarousel.css";
import React, { Component } from "react";
import BookCard from "./BookCard";

export default class BookCarousel extends Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      title: this.props.title,
      titleUrl: this.props.titleUrl,
      collectionUrl: this.props.collectionUrl,
      maxCardsCount: this.props.maxCardsCount ?? 6,
      books: [],
    };
  }

  componentDidMount() {
    fetch(this.state.collectionUrl)
      .then((response) => response.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            books: result.books.slice(0, this.state.maxCardsCount),
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
    const { error, isLoaded, title, books } = this.state;
    const styleEmpty = {
      color: "#0047ab",
      fontSize: "40px",
      fontWeight: "600",
      marginBottom: "10px",
    };
    if (!error) {
      if (isLoaded) {
        if (books.length === 0) {
          return (
            <div className="bookCarousel">
              <a className="bookCarouselTitle" href={this.state.titleUrl}>
                {title}
              </a>
              <hr color="#0047ab" />
              <div className="bookCarouselCards" style={styleEmpty}>
                Скоро будет
              </div>
            </div>
          );
        } else {
          return (
            <div className="bookCarousel">
              <a className="bookCarouselTitle" href={this.state.titleUrl}>
                {title}
              </a>
              <hr color="#0047ab" />
              <div className="bookCarouselCards">
                {books.map((book) => {
                  return <BookCard bookData={book} />;
                })}
              </div>
            </div>
          );
        }
      } else {
        return (
          <div className="bookCarousel">
            <div className="bookCarouselTitle"> {title} </div>
            <div>Loading...</div>
          </div>
        );
      }
    } else {
      <div>Error: {this.state.error.message}</div>;
    }
  }
}
