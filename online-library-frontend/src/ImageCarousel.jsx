import React, { Component } from 'react'
import SimpleImageSlider from "react-simple-image-slider";

export default class ImageCarousel extends Component {
  constructor(props) {
    super(props)
  
    this.state = {
      images: [
        {url: 'pushkin-carusel.png'}, 
        {url: 'gogol-crusel.jpg'},
        {url: 'anton-chekhov.png'}, 
      ],
    }
  }

  render() {
    const { images } = this.state;
    return (
      <div style={{
        marginTop: 35,
      }}>
        <a href='/authors'>
            <SimpleImageSlider
              width={1000}
              height={300}
              images={images.map((image) => image.url)}
              showBullets={true}
              loop={true}
              autoPlay={true}
              autoPlayDelay={ 4 }
            />
        </a>
      </div>
    )
  }
}