<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/olapline/PlanningAnalytics">
    <img src="https://www.olapline.de/files/olapline-theme/img/brand-olapline-subline.svg" alt="Logo" width="300">
  </a>

<h3 align="center">PlanningAnalyticsNET</h3>

  <p align="center">
    This Interface wraps the odata Rest Interface of TM1 in a workable Project in C#.NET.
    <br />
    <a href="https://github.com/olapline/PlanningAnalytics"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/olapline/PlanningAnalytics">View Demo</a>
    ·
    <a href="https://github.com/olapline/PlanningAnalytics/issues">Report Bug</a>
    ·
    <a href="https://github.com/olapline/PlanningAnalytics/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://olapline.de)

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Newtonsoft JSON](https://newtonsoft.org/)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

   ```csharp
            string BaseUrl = "http://localhost:8091/api/v1/";
            string UserName = "XXX";
            string Password = "XXX";


            using (PlanningAnalyticsConnection Connection = new PlanningAnalyticsConnection(BaseUrl))
            {

                if (Connection.Authenticate(UserName, Password))
                {

                    Console.WriteLine("Instance Name:" + Connection.InstanceName);

                    // Iterate over Processes
                    foreach (var process in Connection.Processes)
                    {
                        // The Names will be fetched by the iterator, the object by the getter
                        Console.WriteLine(process.Name);
                        PlanningAnalytics.Model.PlanningAnalyticsProcess Process = Connection.Processes[process.Name];
                        Console.WriteLine(Process.DataSource.Type);

                    }

                    // get a specific Dimension
                    PlanningAnalytics.Model.PlanningAnalyticsDimension Dimension = Connection.Dimensions["Projects"];
                    var Hier = Dimension.Hierarchies.ElementAt(0);
                    foreach (var attr in Hier.ElementAttributes)
                    {
                        Console.WriteLine(attr.Name);
                    }

                    foreach (var level in Hier.Levels)
                    {
                        Console.WriteLine(level.Name);
                    }
                    Console.WriteLine(Dimension.Hierarchies.ElementAt(0).UniqueName + " Number of Elements:" + Hier.Cardinality);



                }

            }
   ```
### Prerequisites


* .net 4.7.2
  

### Installation

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/olapline/PlanningAnalytics.git
   ```

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Feature 1
- [ ] Feature 2
- [ ] Feature 3
    - [ ] Nested Feature

See the [open issues](https://github.com/olapline/PlanningAnalytics/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Your Name - [@twitter_handle](https://twitter.com/twitter_handle) - info@olapline.de@info@olapline.de_client.com

Project Link: [https://github.com/olapline/PlanningAnalytics](https://github.com/olapline/PlanningAnalytics)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/olapline/PlanningAnalytics.svg?style=for-the-badge
[contributors-url]: https://github.com/olapline/PlanningAnalytics/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/olapline/PlanningAnalytics.svg?style=for-the-badge
[forks-url]: https://github.com/olapline/PlanningAnalytics/network/members
[stars-shield]: https://img.shields.io/github/stars/olapline/PlanningAnalytics.svg?style=for-the-badge
[stars-url]: https://github.com/olapline/PlanningAnalytics/stargazers
[issues-shield]: https://img.shields.io/github/issues/olapline/PlanningAnalytics.svg?style=for-the-badge
[issues-url]: https://github.com/olapline/PlanningAnalytics/issues
[license-shield]: https://img.shields.io/github/license/olapline/PlanningAnalytics.svg?style=for-the-badge
[license-url]: https://github.com/olapline/PlanningAnalytics/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/olapline
[product-screenshot]: images/screenshot.png