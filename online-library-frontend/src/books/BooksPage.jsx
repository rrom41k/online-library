import { React, Component } from 'react'
import Search from '../Search'
import BookCard from './BookCard'
import '../css/BooksPage.css'

export default class BooksPage extends Component {
  state = {
      error: null,
      isLoaded: false,
      title: this.props.title,
      titleUrl: this.props.titleUrl,
      collectionUrl: 'http://localhost:7140/api/books?searchTerm=' + 
        ((new URLSearchParams(window.location.search)).get('searchTerm') ?? ''),
      books: []
  }

  componentDidMount() {
    fetch(this.state.collectionUrl)
      .then(response => response.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            books: result
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error
          });
        }
      );
  }

  render() {
    const { error, isLoaded, title, books } = this.state;
    const styleEmpty = {
      display: 'flex',
      justifyContent: 'center',
      color: '#0047ab',
      fontSize: '40px',
      fontWeight: '600',
      marginTop: '20px',
    }
    if (!error) {
      if (isLoaded) {
        if (books.length === 0) {
          return (
          <div className='booksPage'>
            <Search />
              <div style={styleEmpty}>
                По запросу "{(new URLSearchParams(window.location.search)).get('searchTerm')}" ничего не найдено :(
              </div>
          </div>);
        } 
        else {
          return (
            <div className='booksPage'>
              <Search />
              <div className='booksList'>{
              books.map((book) => {
                console.log(book);
                return <BookCard bookData={book} />
              })}</div>
            </div>
            )}
        }
      else {
        return (
          <div className='booksPage'>
            <div className='bookCarouselTitle'> {title} </div>
            <div>Loading...</div>
          </div>
      )
      }
    }
    else {
      <div>Error: {this.state.error.message}</div>
    }
  }
}
