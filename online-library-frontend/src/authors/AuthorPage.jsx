import React, { Component } from "react";
import { useParams } from "react-router-dom";
import "../css/AuthorPageBody.css";
import NotFound from "../NotFound";
import BookCarousel from "../books/BookCarousel";

class AuthorPageBody extends Component {
  constructor(props) {
    super(props);

    this.state = {
      errorStatus: false,
      errorCode: null,
      isLoaded: false,
      slug: this.props.slug,
      author: null,
    };
  }

  componentDidMount() {
    console.log();
    fetch("http://localhost:7140/api/authors/" + this.state.slug)
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
          author: result,
        });
      });
  }

  createFIO(author) {
    return `${author["name"]} ${author["secondName"]} ${author["surname"]}`;
  }

  render() {
    const { errorStatus, isLoaded, author } = this.state;
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
        if (author !== null) {
          return (
            <div className="authorPageBody">
              <div className="authorPageBodyImgDescription">
                <img src={author["imageUrl"]} alt="Фото автора" />
                <h1 className="authorPageBodyTitle">
                  {this.createFIO(author)}
                </h1>
                <div className="authorPageBodyDescription">
                  {author["description"]}
                </div>
              </div>
              <BookCarousel
                title={this.createFIO(author)}
                titleUrl={"/authors/" + author["slug"]}
                collectionUrl={
                  "http://localhost:7140/api/authors/" + author["slug"]
                }
                maxCardsCount={5}
              />
            </div>
          );
        } else {
        }
      } else {
        return (
          <div className="authorPageBody">
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

export default function AuthorPage() {
  const params = useParams();

  return (
    <div>
      <AuthorPageBody slug={params.slug} />
    </div>
  );
}
