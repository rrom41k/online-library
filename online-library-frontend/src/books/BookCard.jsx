import '../css/BookCard.css'
import React, { Component } from 'react'

export default class BookCard extends Component {
  constructor(props) {
    super(props)
  
    this.state = {
      book: props.bookData,
    }
  }

  createFIO(author) {
    return `${author["surname"]} ${author["name"][0]}. ${author["secondName"][0]}.`;
  }
  
  render() {
    const author = this.state.book['author']
    return (
      <div className='bookCard'>
        <a className='bookCardImage' href={`/books/${this.state.book['slug']}`}>
          <img src={this.state.book['coverUrl']} alt='Book Cover'/>
        </a>
        <a href={`/authors/${author['slug']}`}>
          <p className='bookCardAuthor'>
            {this.createFIO(author)}
          </p>
        </a>
        <a href={`/books/${this.state.book['slug']}`}>
          <p className='bookCardTitle' style={{ lineHeight: '22px' }}>
            {this.state.book['title']}
          </p>
        </a>
      </div>
    )
  }
}
