import { useParams } from "react-router-dom";
import BookCarousel from "./BookCarousel";
import React, { Component } from "react";
import NotFound from "../NotFound";
import "../css/BookPageBody.css";

class BookPageBody extends Component {
  constructor(props) {
    super(props);

    this.state = {
      errorStatus: false,
      errorCode: null,
      isLoaded: false,
      slug: this.props.slug,
      book: null,
    };
  }

  componentDidMount() {
    fetch("http://localhost:7140/api/books/" + this.state.slug)
      .then((response) => {
        if (response.status === 200) return response.json();
        else
          this.setState({
            errorStatus: true,
            errorCode: response.status,
          });
      })
      .then((result) => {
        this.setState({
          isLoaded: true,
          book: result,
        });
      });
  }

  render() {
    const { errorStatus, isLoaded, book } = this.state;

    const styleError = {
      marginLeft: "20%",
      marginRight: "20%",
      marginTop: "100px",
      fontSize: "60px",
      display: "flex",
      justifyContent: "center",
      color: "#0047ab",
      fontWeight: "bold",
    };

    if (errorStatus !== true) {
      if (isLoaded) {
        if (book !== null) {
          return (
            <div className="bookPageBody">
              <div className="bookPageBodyImgDescription">
                <div className="bookPageBodyImgGenresAuthor">
                  <img src={book.coverUrl} alt="Обложка Книги" />
                  <div className="bookPageBodyAuthor">
                    <a
                      href={`/authors/${book.author.slug}`}
                    >{`${book.author.surname} ${book.author.name} ${book.author.secondName}`}</a>
                  </div>
                  <ul className="bookPageBodyGenres">
                    {book.genres.map((genre) => {
                      return (
                        <a href={`/books/?searchTerm=${genre.name}`}>
                          <li>{genre.name}</li>
                        </a>
                      );
                    })}
                  </ul>
                </div>
                <h1 className="bookPageBodyTitle">{book.title}</h1>
                <div className="bookPageBodyDescription">
                  {book.description}
                </div>
              </div>
              <BookCarousel
                title={"Другие книги жанра " + book["genres"][0]["name"]}
                titleUrl={"/books/" + book["slug"]}
                collectionUrl={
                  "http://localhost:7140/api/genres/" +
                  book["genres"][0]["slug"]
                }
                maxCardsCount={5}
              />
            </div>
          );
        } else {
        }
      } else {
        return (
          <div className="bookPageBody">
            <div>Loading...</div>
          </div>
        );
      }
    } else {
      if (this.state.errorCode === 404) return <NotFound />;
      else return <div style={styleError}>Error {this.state.errorCode}</div>;
    }
  }
}

function createFIO(author) {
  return `${author["name"]} ${author["secondName"]} ${author["surname"]}`;
}

export default function BookPage() {
  const params = useParams();

  return (
    <div>
      <BookPageBody slug={params.slug} />
    </div>
  );
}
