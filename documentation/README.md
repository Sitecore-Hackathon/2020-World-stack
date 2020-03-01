**Sitecore Hackathon**

**Team:** WorldStack
**Topic chosen:** Sitecore Hackathon Website
**Purpose to choose** : We found hackathon site to be really interesting to work on as we found so many ideas on how to improve it.

This site has developed using Sitecore 9.3 and with below list of features.

1. Sitecore Helix Solution Structure
2. SxA Theme development
3. SxA Pages and Components
4. Sitecore Form integration
5. Unicorn Integration
6. External API (GitHub) client to get repository details
7. Swiper/Vanilla Tilt JS

**Pages:**

**Home Page**

This is main page for the site, it displays all the important information regarding Sitecore Hackathon.

![Image description](https://github.com/Sitecore-Hackathon/2020-World-stack/blob/master/documentation/images/PAGE1.png)

**Hackathon Pages**

These pages display the information of the previous Sitecore hackathons.

**Registration Form**

This form allows to register for the current year&#39;s hackathon.

![Image description](https://github.com/Sitecore-Hackathon/2020-World-stack/blob/master/documentation/images/PAGE2.png)

**Components:**

**GitHub Cards**

A dynamic component that lists all the repositories within an account. It filters repositories based on Name (ex: &#39;2020-&#39; will list all the repos prefixed with 2020). It shows information regarding last updated, forks, stars etc. There is a rendering property through which winning teams can be highlighted.
It requires accessToken from Github in order to access Github Client api. Please update it in WorldStack.Feature.GithubRepo.config

![Image description](https://github.com/Sitecore-Hackathon/2020-World-stack/blob/master/documentation/images/Capture1.PNG)



**FAQs**

This page provides with the most frequently asked questions and their answers in a cool way.

**Customization of SxA Components**

Customization of SxA header, footer, Navigation, Accordion and Title. They inherit a new set of styles that corresponds to Sitecore Hackathon colors.

**Future Work**

We are aiming to extend the Sitecore Hackathon site with the following features:

1. Provide an interactive map to show the teams geographically
2. Provide a board for the judges to automate the scoring process and to select the top winners
3. Provide an automatic emailing system (notifications) using custom contact list and Email Exp Manager
4. Feature to search Sitecore Hackathon Github Repo especially code lines using Github client which will be very useful for Sitecore developers ( Server code created in Foundations )
