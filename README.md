






<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
  <link rel="dns-prefetch" href="https://github.githubassets.com">
  <link rel="dns-prefetch" href="https://avatars0.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars1.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars2.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars3.githubusercontent.com">
  <link rel="dns-prefetch" href="https://github-cloud.s3.amazonaws.com">
  <link rel="dns-prefetch" href="https://user-images.githubusercontent.com/">



  <link crossorigin="anonymous" media="all" integrity="sha512-5Bs4ERl99/u2AUfpOZF2F0cdfNby7+Vd9teUshXUBPo5CjwECR7IAEf+weI/eCk5tF7K1h3O8hd8k0+P/HePeg==" rel="stylesheet" href="https://github.githubassets.com/assets/frameworks-e41b3811197df7fbb60147e939917617.css" />
  
    <link crossorigin="anonymous" media="all" integrity="sha512-whJUL2x2LCRTRARm7cOI7JLt7r+fON3F+LANQKvZarg3YuJttlZQnErXU2uAq06K5Io1Bcv6eWF64jH1L8Lbrg==" rel="stylesheet" href="https://github.githubassets.com/assets/github-c212542f6c762c2453440466edc388ec.css" />
    
    
    
    


  <meta name="viewport" content="width=device-width">
  
  <title>googlesamples/unity-jar-resolver: Unity plugin which resolves Android &amp; iOS dependencies and performs version management</title>
    <meta name="description" content="Unity plugin which resolves Android &amp; iOS dependencies and performs version management - googlesamples/unity-jar-resolver">
    <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub">
  <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub">
  <meta property="fb:app_id" content="1401488693436528">

    <meta name="twitter:image:src" content="https://avatars3.githubusercontent.com/u/7378196?s=400&amp;v=4" /><meta name="twitter:site" content="@github" /><meta name="twitter:card" content="summary" /><meta name="twitter:title" content="googlesamples/unity-jar-resolver" /><meta name="twitter:description" content="Unity plugin which resolves Android &amp; iOS dependencies and performs version management - googlesamples/unity-jar-resolver" />
    <meta property="og:image" content="https://avatars3.githubusercontent.com/u/7378196?s=400&amp;v=4" /><meta property="og:site_name" content="GitHub" /><meta property="og:type" content="object" /><meta property="og:title" content="googlesamples/unity-jar-resolver" /><meta property="og:url" content="https://github.com/googlesamples/unity-jar-resolver" /><meta property="og:description" content="Unity plugin which resolves Android &amp; iOS dependencies and performs version management - googlesamples/unity-jar-resolver" />

  <link rel="assets" href="https://github.githubassets.com/">
  <link rel="web-socket" href="wss://live.github.com/_sockets/VjI6NTE0ODM1Mjc3OjE1MmNkYjkxZTkxY2E3ZTkxMmQ0MTgzYTExOTA3ZWRjZmYyMzMyY2IxMDVmZjFkZjU5NzNiNjc4M2NlZmZkOGM=--af68eda4611a4f239ed3524280008aa592ae30fb">
  <link rel="sudo-modal" href="/sessions/sudo_modal">

  <meta name="request-id" content="E6B0:1A8F:DB3F9:105F1F:5E7AE864" data-pjax-transient="true" /><meta name="html-safe-nonce" content="66db454d72140838046d6a62d53568feaf31373f" data-pjax-transient="true" /><meta name="visitor-payload" content="eyJyZWZlcnJlciI6Imh0dHBzOi8vZ2l0aHViLmNvbS9nb29nbGVzYW1wbGVzP3F1ZXJ5PWlkZW50aXR5LXRvb2xraXQiLCJyZXF1ZXN0X2lkIjoiRTZCMDoxQThGOkRCM0Y5OjEwNUYxRjo1RTdBRTg2NCIsInZpc2l0b3JfaWQiOiI1OTA1MjgxNjI3Nzg4NTk1MzcwIiwicmVnaW9uX2VkZ2UiOiJhcC1ub3J0aGVhc3QtMiIsInJlZ2lvbl9yZW5kZXIiOiJpYWQifQ==" data-pjax-transient="true" /><meta name="visitor-hmac" content="712ad45ada37ed8cf3ce87db6f0084065f2606f56dc780a4c84871f4ef6e2dba" data-pjax-transient="true" />



  <meta name="github-keyboard-shortcuts" content="repository" data-pjax-transient="true" />

  

  <meta name="selected-link" value="repo_source" data-pjax-transient>

    <meta name="google-site-verification" content="KT5gs8h0wvaagLKAVWq8bbeNwnZZK1r1XQysX3xurLU">
  <meta name="google-site-verification" content="ZzhVyEFwb7w3e0-uOTltm8Jsck2F5StVihD0exw2fsA">
  <meta name="google-site-verification" content="GXs5KoUUkNCoaAZn7wPN-t01Pywp9M3sEjnt_3_ZWPc">

<meta name="octolytics-host" content="collector.githubapp.com" /><meta name="octolytics-app-id" content="github" /><meta name="octolytics-event-url" content="https://collector.githubapp.com/github-external/browser_event" /><meta name="octolytics-dimension-ga_id" content="" class="js-octo-ga-id" /><meta name="octolytics-actor-id" content="62635306" /><meta name="octolytics-actor-login" content="sakura68" /><meta name="octolytics-actor-hash" content="32a84f58043cffabeab3fb07e58dcf3f62abae9b772f39c0b4bc967af821d250" />
<meta name="analytics-location" content="/&lt;user-name&gt;/&lt;repo-name&gt;" data-pjax-transient="true" />




  <meta class="js-ga-set" name="userId" content="9f4e030404842a7c5fd8fc16b0a8b7a4">

<meta class="js-ga-set" name="dimension1" content="Logged In">



  

      <meta name="hostname" content="github.com">
    <meta name="user-login" content="sakura68">

      <meta name="expected-hostname" content="github.com">

      <meta name="js-proxy-site-detection-payload" content="MWM4MmYxM2U2MTYzNzMwMzRiOTA4YzZjNTBlYjc1ZmFiOWM0YTY4NTgyZTUxZWU3NDVkNmI4NmRkN2MxZTEzZHx7InJlbW90ZV9hZGRyZXNzIjoiMTgyLjIyNC4xMTkuMTg2IiwicmVxdWVzdF9pZCI6IkU2QjA6MUE4RjpEQjNGOToxMDVGMUY6NUU3QUU4NjQiLCJ0aW1lc3RhbXAiOjE1ODUxMTMxOTYsImhvc3QiOiJnaXRodWIuY29tIn0=">

    <meta name="enabled-features" content="MARKETPLACE_FEATURED_BLOG_POSTS,MARKETPLACE_INVOICED_BILLING,MARKETPLACE_SOCIAL_PROOF_CUSTOMERS,MARKETPLACE_TRENDING_SOCIAL_PROOF,MARKETPLACE_RECOMMENDATIONS,MARKETPLACE_PENDING_INSTALLATIONS,RELATED_ISSUES,GHE_CLOUD_TRIAL,PAGE_STALE_CHECK">

  <meta http-equiv="x-pjax-version" content="9d2e5c50e62b8f4ec909afc553a52bf5">
  

      <link href="https://github.com/googlesamples/unity-jar-resolver/commits/master.atom" rel="alternate" title="Recent Commits to unity-jar-resolver:master" type="application/atom+xml">

  <meta name="go-import" content="github.com/googlesamples/unity-jar-resolver git https://github.com/googlesamples/unity-jar-resolver.git">

  <meta name="octolytics-dimension-user_id" content="7378196" /><meta name="octolytics-dimension-user_login" content="googlesamples" /><meta name="octolytics-dimension-repository_id" content="49642334" /><meta name="octolytics-dimension-repository_nwo" content="googlesamples/unity-jar-resolver" /><meta name="octolytics-dimension-repository_public" content="true" /><meta name="octolytics-dimension-repository_is_fork" content="false" /><meta name="octolytics-dimension-repository_network_root_id" content="49642334" /><meta name="octolytics-dimension-repository_network_root_nwo" content="googlesamples/unity-jar-resolver" /><meta name="octolytics-dimension-repository_explore_github_marketplace_ci_cta_shown" content="false" />


    <link rel="canonical" href="https://github.com/googlesamples/unity-jar-resolver" data-pjax-transient>


  <meta name="browser-stats-url" content="https://api.github.com/_private/browser/stats">

  <meta name="browser-errors-url" content="https://api.github.com/_private/browser/errors">

  <link rel="mask-icon" href="https://github.githubassets.com/pinned-octocat.svg" color="#000000">
  <link rel="icon" type="image/x-icon" class="js-site-favicon" href="https://github.githubassets.com/favicon.ico">

<meta name="theme-color" content="#1e2327">


  <link rel="manifest" href="/manifest.json" crossOrigin="use-credentials">

  </head>

  <body class="logged-in env-production min-width-lg">
    

  <div class="position-relative js-header-wrapper ">
    <a href="#start-of-content" class="p-3 bg-blue text-white show-on-focus js-skip-to-content">Skip to content</a>
    <span class="Progress progress-pjax-loader position-fixed width-full js-pjax-loader-bar">
      <span class="progress-pjax-loader-bar top-0 left-0" style="width: 0%;"></span>
    </span>

    
    



        <header class="Header" role="banner">

  <div class="Header-item">
    <a class="Header-link" href="https://github.com/" data-hotkey="g d"
  aria-label="Homepage " data-ga-click="Header, go to dashboard, icon:logo">
  <svg class="octicon octicon-mark-github v-align-middle" height="32" viewBox="0 0 16 16" version="1.1" width="32" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z"/></svg>
</a>

  </div>


  <div class="Header-item Header-item--full">
      <div class="header-search mr-3 scoped-search site-scoped-search js-site-search position-relative js-jump-to"
  role="combobox"
  aria-owns="jump-to-results"
  aria-label="Search or jump to"
  aria-haspopup="listbox"
  aria-expanded="false"
>
  <div class="position-relative">
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-site-search-form" role="search" aria-label="Site" data-scope-type="Repository" data-scope-id="49642334" data-scoped-search-url="/googlesamples/unity-jar-resolver/search" data-unscoped-search-url="/search" action="/googlesamples/unity-jar-resolver/search" accept-charset="UTF-8" method="get">
      <label class="form-control input-sm header-search-wrapper p-0 header-search-wrapper-jump-to position-relative d-flex flex-justify-between flex-items-center js-chromeless-input-container">
        <input type="text"
          class="form-control input-sm header-search-input jump-to-field js-jump-to-field js-site-search-focus js-site-search-field is-clearable"
          data-hotkey="s,/"
          name="q"
          value=""
          placeholder="Search or jump to…"
          data-unscoped-placeholder="Search or jump to…"
          data-scoped-placeholder="Search or jump to…"
          autocapitalize="off"
          aria-autocomplete="list"
          aria-controls="jump-to-results"
          aria-label="Search or jump to…"
          data-jump-to-suggestions-path="/_graphql/GetSuggestedNavigationDestinations"
          spellcheck="false"
          autocomplete="off"
          >
          <input type="hidden" value="k3/9J6eSizkMkD/hWBZn9R8JtvGDMx1c3pQrrcygrWeVQzICxdWyWPtQm4dXU+BA/B6XvX9oo+8EGt9xNYuuVA==" data-csrf="true" class="js-data-jump-to-suggestions-path-csrf" />
          <input type="hidden" class="js-site-search-type-field" name="type" >
            <img src="https://github.githubassets.com/images/search-key-slash.svg" alt="" class="mr-2 header-search-key-slash">

            <div class="Box position-absolute overflow-hidden d-none jump-to-suggestions js-jump-to-suggestions-container">
              
<ul class="d-none js-jump-to-suggestions-template-container">
  

<li class="d-flex flex-justify-start flex-items-center p-0 f5 navigation-item js-navigation-item js-jump-to-suggestion" role="option">
  <a tabindex="-1" class="no-underline d-flex flex-auto flex-items-center jump-to-suggestions-path js-jump-to-suggestion-path js-navigation-open p-2" href="">
    <div class="jump-to-octicon js-jump-to-octicon flex-shrink-0 mr-2 text-center d-none">
      <svg height="16" width="16" class="octicon octicon-repo flex-shrink-0 js-jump-to-octicon-repo d-none" title="Repository" aria-label="Repository" viewBox="0 0 12 16" version="1.1" role="img"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-project flex-shrink-0 js-jump-to-octicon-project d-none" title="Project" aria-label="Project" viewBox="0 0 15 16" version="1.1" role="img"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 00-1 1v14a1 1 0 001 1h13a1 1 0 001-1V1a1 1 0 00-1-1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-search flex-shrink-0 js-jump-to-octicon-search d-none" title="Search" aria-label="Search" viewBox="0 0 16 16" version="1.1" role="img"><path fill-rule="evenodd" d="M15.7 13.3l-3.81-3.83A5.93 5.93 0 0013 6c0-3.31-2.69-6-6-6S1 2.69 1 6s2.69 6 6 6c1.3 0 2.48-.41 3.47-1.11l3.83 3.81c.19.2.45.3.7.3.25 0 .52-.09.7-.3a.996.996 0 000-1.41v.01zM7 10.7c-2.59 0-4.7-2.11-4.7-4.7 0-2.59 2.11-4.7 4.7-4.7 2.59 0 4.7 2.11 4.7 4.7 0 2.59-2.11 4.7-4.7 4.7z"/></svg>
    </div>

    <img class="avatar mr-2 flex-shrink-0 js-jump-to-suggestion-avatar d-none" alt="" aria-label="Team" src="" width="28" height="28">

    <div class="jump-to-suggestion-name js-jump-to-suggestion-name flex-auto overflow-hidden text-left no-wrap css-truncate css-truncate-target">
    </div>

    <div class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none js-jump-to-badge-search">
      <span class="js-jump-to-badge-search-text-default d-none" aria-label="in this repository">
        In this repository
      </span>
      <span class="js-jump-to-badge-search-text-global d-none" aria-label="in all of GitHub">
        All GitHub
      </span>
      <span aria-hidden="true" class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>

    <div aria-hidden="true" class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none d-on-nav-focus js-jump-to-badge-jump">
      Jump to
      <span class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>
  </a>
</li>

</ul>

<ul class="d-none js-jump-to-no-results-template-container">
  <li class="d-flex flex-justify-center flex-items-center f5 d-none js-jump-to-suggestion p-2">
    <span class="text-gray">No suggested jump to results</span>
  </li>
</ul>

<ul id="jump-to-results" role="listbox" class="p-0 m-0 js-navigation-container jump-to-suggestions-results-container js-jump-to-suggestions-results-container">
  

<li class="d-flex flex-justify-start flex-items-center p-0 f5 navigation-item js-navigation-item js-jump-to-scoped-search d-none" role="option">
  <a tabindex="-1" class="no-underline d-flex flex-auto flex-items-center jump-to-suggestions-path js-jump-to-suggestion-path js-navigation-open p-2" href="">
    <div class="jump-to-octicon js-jump-to-octicon flex-shrink-0 mr-2 text-center d-none">
      <svg height="16" width="16" class="octicon octicon-repo flex-shrink-0 js-jump-to-octicon-repo d-none" title="Repository" aria-label="Repository" viewBox="0 0 12 16" version="1.1" role="img"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-project flex-shrink-0 js-jump-to-octicon-project d-none" title="Project" aria-label="Project" viewBox="0 0 15 16" version="1.1" role="img"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 00-1 1v14a1 1 0 001 1h13a1 1 0 001-1V1a1 1 0 00-1-1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-search flex-shrink-0 js-jump-to-octicon-search d-none" title="Search" aria-label="Search" viewBox="0 0 16 16" version="1.1" role="img"><path fill-rule="evenodd" d="M15.7 13.3l-3.81-3.83A5.93 5.93 0 0013 6c0-3.31-2.69-6-6-6S1 2.69 1 6s2.69 6 6 6c1.3 0 2.48-.41 3.47-1.11l3.83 3.81c.19.2.45.3.7.3.25 0 .52-.09.7-.3a.996.996 0 000-1.41v.01zM7 10.7c-2.59 0-4.7-2.11-4.7-4.7 0-2.59 2.11-4.7 4.7-4.7 2.59 0 4.7 2.11 4.7 4.7 0 2.59-2.11 4.7-4.7 4.7z"/></svg>
    </div>

    <img class="avatar mr-2 flex-shrink-0 js-jump-to-suggestion-avatar d-none" alt="" aria-label="Team" src="" width="28" height="28">

    <div class="jump-to-suggestion-name js-jump-to-suggestion-name flex-auto overflow-hidden text-left no-wrap css-truncate css-truncate-target">
    </div>

    <div class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none js-jump-to-badge-search">
      <span class="js-jump-to-badge-search-text-default d-none" aria-label="in this repository">
        In this repository
      </span>
      <span class="js-jump-to-badge-search-text-global d-none" aria-label="in all of GitHub">
        All GitHub
      </span>
      <span aria-hidden="true" class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>

    <div aria-hidden="true" class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none d-on-nav-focus js-jump-to-badge-jump">
      Jump to
      <span class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>
  </a>
</li>

  

<li class="d-flex flex-justify-start flex-items-center p-0 f5 navigation-item js-navigation-item js-jump-to-global-search d-none" role="option">
  <a tabindex="-1" class="no-underline d-flex flex-auto flex-items-center jump-to-suggestions-path js-jump-to-suggestion-path js-navigation-open p-2" href="">
    <div class="jump-to-octicon js-jump-to-octicon flex-shrink-0 mr-2 text-center d-none">
      <svg height="16" width="16" class="octicon octicon-repo flex-shrink-0 js-jump-to-octicon-repo d-none" title="Repository" aria-label="Repository" viewBox="0 0 12 16" version="1.1" role="img"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-project flex-shrink-0 js-jump-to-octicon-project d-none" title="Project" aria-label="Project" viewBox="0 0 15 16" version="1.1" role="img"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 00-1 1v14a1 1 0 001 1h13a1 1 0 001-1V1a1 1 0 00-1-1z"/></svg>
      <svg height="16" width="16" class="octicon octicon-search flex-shrink-0 js-jump-to-octicon-search d-none" title="Search" aria-label="Search" viewBox="0 0 16 16" version="1.1" role="img"><path fill-rule="evenodd" d="M15.7 13.3l-3.81-3.83A5.93 5.93 0 0013 6c0-3.31-2.69-6-6-6S1 2.69 1 6s2.69 6 6 6c1.3 0 2.48-.41 3.47-1.11l3.83 3.81c.19.2.45.3.7.3.25 0 .52-.09.7-.3a.996.996 0 000-1.41v.01zM7 10.7c-2.59 0-4.7-2.11-4.7-4.7 0-2.59 2.11-4.7 4.7-4.7 2.59 0 4.7 2.11 4.7 4.7 0 2.59-2.11 4.7-4.7 4.7z"/></svg>
    </div>

    <img class="avatar mr-2 flex-shrink-0 js-jump-to-suggestion-avatar d-none" alt="" aria-label="Team" src="" width="28" height="28">

    <div class="jump-to-suggestion-name js-jump-to-suggestion-name flex-auto overflow-hidden text-left no-wrap css-truncate css-truncate-target">
    </div>

    <div class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none js-jump-to-badge-search">
      <span class="js-jump-to-badge-search-text-default d-none" aria-label="in this repository">
        In this repository
      </span>
      <span class="js-jump-to-badge-search-text-global d-none" aria-label="in all of GitHub">
        All GitHub
      </span>
      <span aria-hidden="true" class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>

    <div aria-hidden="true" class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none d-on-nav-focus js-jump-to-badge-jump">
      Jump to
      <span class="d-inline-block ml-1 v-align-middle">↵</span>
    </div>
  </a>
</li>


    <li class="d-flex flex-justify-center flex-items-center p-0 f5 js-jump-to-suggestion">
      <img src="https://github.githubassets.com/images/spinners/octocat-spinner-128.gif" alt="Octocat Spinner Icon" class="m-2" width="28">
    </li>
</ul>

            </div>
      </label>
</form>  </div>
</div>


      <nav class="d-flex" aria-label="Global">


  <a class="js-selected-navigation-item Header-link  mr-3" data-hotkey="g p" data-ga-click="Header, click, Nav menu - item:pulls context:user" aria-label="Pull requests you created" data-selected-links="/pulls /pulls/assigned /pulls/mentioned /pulls" href="/pulls">
    Pull requests
</a>
  <a class="js-selected-navigation-item Header-link  mr-3" data-hotkey="g i" data-ga-click="Header, click, Nav menu - item:issues context:user" aria-label="Issues you created" data-selected-links="/issues /issues/assigned /issues/mentioned /issues" href="/issues">
    Issues
</a>
    <div class="mr-3">
      <a class="js-selected-navigation-item Header-link" data-ga-click="Header, click, Nav menu - item:marketplace context:user" data-octo-click="marketplace_click" data-octo-dimensions="location:nav_bar" data-selected-links=" /marketplace" href="/marketplace">
        Marketplace
</a>      

    </div>

  <a class="js-selected-navigation-item Header-link  mr-3" data-ga-click="Header, click, Nav menu - item:explore" data-selected-links="/explore /trending /trending/developers /integrations /integrations/feature/code /integrations/feature/collaborate /integrations/feature/ship showcases showcases_search showcases_landing /explore" href="/explore">
    Explore
</a>

</nav>

  </div>


  <div class="Header-item">
    

    <a aria-label="You have no unread notifications" class="Header-link notification-indicator position-relative tooltipped tooltipped-sw js-socket-channel js-notification-indicator" data-hotkey="g n" data-ga-click="Header, go to notifications, icon:read" data-channel="notification-changed:62635306" href="/notifications/beta">
        <span class="js-indicator-modifier mail-status "></span>
        <svg class="octicon octicon-bell" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M14 12v1H0v-1l.73-.58c.77-.77.81-2.55 1.19-4.42C2.69 3.23 6 2 6 2c0-.55.45-1 1-1s1 .45 1 1c0 0 3.39 1.23 4.16 5 .38 1.88.42 3.66 1.19 4.42l.66.58H14zm-7 4c1.11 0 2-.89 2-2H5c0 1.11.89 2 2 2z"/></svg>
</a>
  </div>


  <div class="Header-item position-relative">
    <details class="details-overlay details-reset">
  <summary class="Header-link"
      aria-label="Create new…"
      data-ga-click="Header, create new, icon:add">
    <svg class="octicon octicon-plus" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 9H7v5H5V9H0V7h5V2h2v5h5v2z"/></svg> <span class="dropdown-caret"></span>
  </summary>
  <details-menu class="dropdown-menu dropdown-menu-sw">
    
<a role="menuitem" class="dropdown-item" href="/new" data-ga-click="Header, create new repository">
  New repository
</a>

  <a role="menuitem" class="dropdown-item" href="/new/import" data-ga-click="Header, import a repository">
    Import repository
  </a>

<a role="menuitem" class="dropdown-item" href="https://gist.github.com/" data-ga-click="Header, create new gist">
  New gist
</a>

  <a role="menuitem" class="dropdown-item" href="/organizations/new" data-ga-click="Header, create new organization">
    New organization
  </a>


  <div role="none" class="dropdown-divider"></div>
  <div class="dropdown-header">
    <span title="googlesamples/unity-jar-resolver">This repository</span>
  </div>
    <a role="menuitem" class="dropdown-item" href="/googlesamples/unity-jar-resolver/issues/new/choose" data-ga-click="Header, create new issue" data-skip-pjax>
      New issue
    </a>


  </details-menu>
</details>

  </div>

  <div class="Header-item position-relative mr-0">
    
  <details class="details-overlay details-reset js-feature-preview-indicator-container" data-feature-preview-indicator-src="/users/sakura68/feature_preview/indicator_check.json">

  <summary class="Header-link"
    aria-label="View profile and more"
    data-ga-click="Header, show menu, icon:avatar">
    <img class="avatar " alt="@sakura68" width="20" height="20" src="https://avatars0.githubusercontent.com/u/62635306?s=60&amp;v=4">


      <span class="feature-preview-indicator js-feature-preview-indicator" hidden></span>
    <span class="dropdown-caret"></span>
  </summary>
  <details-menu class="dropdown-menu dropdown-menu-sw mt-2" style="width: 180px">
    <div class="header-nav-current-user css-truncate"><a role="menuitem" class="no-underline user-profile-link px-3 pt-2 pb-2 mb-n2 mt-n1 d-block" href="/sakura68" data-ga-click="Header, go to profile, text:Signed in as">Signed in as <strong class="css-truncate-target">sakura68</strong></a></div>
    <div role="none" class="dropdown-divider"></div>

      <div class="pl-3 pr-3 f6 user-status-container js-user-status-context pb-1" data-url="/users/status?compact=1&amp;link_mentions=0&amp;truncate=1">
        
<div class="js-user-status-container
    user-status-compact rounded-1 px-2 py-1 mt-2
    border
  " data-team-hovercards-enabled>
  <details class="js-user-status-details details-reset details-overlay details-overlay-dark">
    <summary class="btn-link btn-block link-gray no-underline js-toggle-user-status-edit toggle-user-status-edit "
      role="menuitem" data-hydro-click="{&quot;event_type&quot;:&quot;user_profile.click&quot;,&quot;payload&quot;:{&quot;profile_user_id&quot;:7378196,&quot;target&quot;:&quot;EDIT_USER_STATUS&quot;,&quot;user_id&quot;:62635306,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;}}" data-hydro-click-hmac="92ce75c626f9f19df17f0671f104a3fcdd6e389e1f74f9d4db013f82d11f4de6">
      <div class="d-flex">
        <div class="f6 lh-condensed user-status-header
          d-inline-block v-align-middle
            user-status-emoji-only-header circle
            pr-2
"
            style="max-width: 29px"
          >
          <div class="user-status-emoji-container flex-shrink-0 mr-1 mt-1 lh-condensed-ultra v-align-bottom" style="">
            <svg class="octicon octicon-smiley" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8s3.58 8 8 8 8-3.58 8-8-3.58-8-8-8zm4.81 12.81a6.72 6.72 0 01-2.17 1.45c-.83.36-1.72.53-2.64.53-.92 0-1.81-.17-2.64-.53-.81-.34-1.55-.83-2.17-1.45a6.773 6.773 0 01-1.45-2.17A6.59 6.59 0 011.21 8c0-.92.17-1.81.53-2.64.34-.81.83-1.55 1.45-2.17.62-.62 1.36-1.11 2.17-1.45A6.59 6.59 0 018 1.21c.92 0 1.81.17 2.64.53.81.34 1.55.83 2.17 1.45.62.62 1.11 1.36 1.45 2.17.36.83.53 1.72.53 2.64 0 .92-.17 1.81-.53 2.64-.34.81-.83 1.55-1.45 2.17zM4 6.8v-.59c0-.66.53-1.19 1.2-1.19h.59c.66 0 1.19.53 1.19 1.19v.59c0 .67-.53 1.2-1.19 1.2H5.2C4.53 8 4 7.47 4 6.8zm5 0v-.59c0-.66.53-1.19 1.2-1.19h.59c.66 0 1.19.53 1.19 1.19v.59c0 .67-.53 1.2-1.19 1.2h-.59C9.53 8 9 7.47 9 6.8zm4 3.2c-.72 1.88-2.91 3-5 3s-4.28-1.13-5-3c-.14-.39.23-1 .66-1h8.59c.41 0 .89.61.75 1z"/></svg>
          </div>
        </div>
        <div class="
          d-inline-block v-align-middle
          
          
           css-truncate css-truncate-target 
           user-status-message-wrapper f6"
           style="line-height: 20px;" >
          <div class="d-inline-block text-gray-dark v-align-text-top text-left">
              <span class="text-gray ml-2">Set status</span>
          </div>
        </div>
      </div>
    </summary>
    <details-dialog class="details-dialog rounded-1 anim-fade-in fast Box Box--overlay" role="dialog" tabindex="-1">
      <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="position-relative flex-auto js-user-status-form" action="/users/status?compact=1&amp;link_mentions=0&amp;truncate=1" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" /><input type="hidden" name="authenticity_token" value="iMw/SaDnbU3XomwiTCaVeNVM0k5Ajo0RPq8ka8l+FxbSj67R0UXeZ93gUVzQ3T8KJ94O+84wxV0DCQsQGofkug==" />
        <div class="Box-header bg-gray border-bottom p-3">
          <button class="Box-btn-octicon js-toggle-user-status-edit btn-octicon float-right" type="reset" aria-label="Close dialog" data-close-dialog>
            <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
          </button>
          <h3 class="Box-title f5 text-bold text-gray-dark">Edit status</h3>
        </div>
        <input type="hidden" name="emoji" class="js-user-status-emoji-field" value="">
        <input type="hidden" name="organization_id" class="js-user-status-org-id-field" value="">
        <div class="px-3 py-2 text-gray-dark">
          <div class="js-characters-remaining-container position-relative mt-2">
            <div class="input-group d-table form-group my-0 js-user-status-form-group">
              <span class="input-group-button d-table-cell v-align-middle" style="width: 1%">
                <button type="button" aria-label="Choose an emoji" class="btn-outline btn js-toggle-user-status-emoji-picker btn-open-emoji-picker p-0">
                  <span class="js-user-status-original-emoji" hidden></span>
                  <span class="js-user-status-custom-emoji"></span>
                  <span class="js-user-status-no-emoji-icon" >
                    <svg class="octicon octicon-smiley" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8s3.58 8 8 8 8-3.58 8-8-3.58-8-8-8zm4.81 12.81a6.72 6.72 0 01-2.17 1.45c-.83.36-1.72.53-2.64.53-.92 0-1.81-.17-2.64-.53-.81-.34-1.55-.83-2.17-1.45a6.773 6.773 0 01-1.45-2.17A6.59 6.59 0 011.21 8c0-.92.17-1.81.53-2.64.34-.81.83-1.55 1.45-2.17.62-.62 1.36-1.11 2.17-1.45A6.59 6.59 0 018 1.21c.92 0 1.81.17 2.64.53.81.34 1.55.83 2.17 1.45.62.62 1.11 1.36 1.45 2.17.36.83.53 1.72.53 2.64 0 .92-.17 1.81-.53 2.64-.34.81-.83 1.55-1.45 2.17zM4 6.8v-.59c0-.66.53-1.19 1.2-1.19h.59c.66 0 1.19.53 1.19 1.19v.59c0 .67-.53 1.2-1.19 1.2H5.2C4.53 8 4 7.47 4 6.8zm5 0v-.59c0-.66.53-1.19 1.2-1.19h.59c.66 0 1.19.53 1.19 1.19v.59c0 .67-.53 1.2-1.19 1.2h-.59C9.53 8 9 7.47 9 6.8zm4 3.2c-.72 1.88-2.91 3-5 3s-4.28-1.13-5-3c-.14-.39.23-1 .66-1h8.59c.41 0 .89.61.75 1z"/></svg>
                  </span>
                </button>
              </span>
              <text-expander keys=": @" data-mention-url="/autocomplete/user-suggestions" data-emoji-url="/autocomplete/emoji">
                <input
                  type="text"
                  autocomplete="off"
                  data-no-org-url="/autocomplete/user-suggestions"
                  data-org-url="/suggestions?mention_suggester=1"
                  data-maxlength="80"
                  class="d-table-cell width-full form-control js-user-status-message-field js-characters-remaining-field"
                  placeholder="What's happening?"
                  name="message"
                  value=""
                  aria-label="What is your current status?">
              </text-expander>
              <div class="error">Could not update your status, please try again.</div>
            </div>
            <div style="margin-left: 53px" class="my-1 text-small label-characters-remaining js-characters-remaining" data-suffix="remaining" hidden>
              80 remaining
            </div>
          </div>
          <include-fragment class="js-user-status-emoji-picker" data-url="/users/status/emoji"></include-fragment>
          <div class="overflow-auto ml-n3 mr-n3 px-3 border-bottom" style="max-height: 33vh">
            <div class="user-status-suggestions js-user-status-suggestions collapsed overflow-hidden">
              <h4 class="f6 text-normal my-3">Suggestions:</h4>
              <div class="mx-3 mt-2 clearfix">
                  <div class="float-left col-6">
                      <button type="button" value=":palm_tree:" class="d-flex flex-items-baseline flex-items-stretch lh-condensed f6 btn-link link-gray no-underline js-predefined-user-status mb-1">
                        <div class="emoji-status-width mr-2 v-align-middle js-predefined-user-status-emoji">
                          <g-emoji alias="palm_tree" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f334.png">🌴</g-emoji>
                        </div>
                        <div class="d-flex flex-items-center no-underline js-predefined-user-status-message ws-normal text-left" style="border-left: 1px solid transparent">
                          On vacation
                        </div>
                      </button>
                      <button type="button" value=":face_with_thermometer:" class="d-flex flex-items-baseline flex-items-stretch lh-condensed f6 btn-link link-gray no-underline js-predefined-user-status mb-1">
                        <div class="emoji-status-width mr-2 v-align-middle js-predefined-user-status-emoji">
                          <g-emoji alias="face_with_thermometer" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f912.png">🤒</g-emoji>
                        </div>
                        <div class="d-flex flex-items-center no-underline js-predefined-user-status-message ws-normal text-left" style="border-left: 1px solid transparent">
                          Out sick
                        </div>
                      </button>
                  </div>
                  <div class="float-left col-6">
                      <button type="button" value=":house:" class="d-flex flex-items-baseline flex-items-stretch lh-condensed f6 btn-link link-gray no-underline js-predefined-user-status mb-1">
                        <div class="emoji-status-width mr-2 v-align-middle js-predefined-user-status-emoji">
                          <g-emoji alias="house" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f3e0.png">🏠</g-emoji>
                        </div>
                        <div class="d-flex flex-items-center no-underline js-predefined-user-status-message ws-normal text-left" style="border-left: 1px solid transparent">
                          Working from home
                        </div>
                      </button>
                      <button type="button" value=":dart:" class="d-flex flex-items-baseline flex-items-stretch lh-condensed f6 btn-link link-gray no-underline js-predefined-user-status mb-1">
                        <div class="emoji-status-width mr-2 v-align-middle js-predefined-user-status-emoji">
                          <g-emoji alias="dart" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f3af.png">🎯</g-emoji>
                        </div>
                        <div class="d-flex flex-items-center no-underline js-predefined-user-status-message ws-normal text-left" style="border-left: 1px solid transparent">
                          Focusing
                        </div>
                      </button>
                  </div>
              </div>
            </div>
            <div class="user-status-limited-availability-container">
              <div class="form-checkbox my-0">
                <input type="checkbox" name="limited_availability" value="1" class="js-user-status-limited-availability-checkbox" data-default-message="I may be slow to respond." aria-describedby="limited-availability-help-text-truncate-true-compact-true" id="limited-availability-truncate-true-compact-true">
                <label class="d-block f5 text-gray-dark mb-1" for="limited-availability-truncate-true-compact-true">
                  Busy
                </label>
                <p class="note" id="limited-availability-help-text-truncate-true-compact-true">
                  When others mention you, assign you, or request your review,
                  GitHub will let them know that you have limited availability.
                </p>
              </div>
            </div>
          </div>
          <div class="d-inline-block f5 mr-2 pt-3 pb-2" >
  <div class="d-inline-block mr-1">
    Clear status
  </div>

  <details class="js-user-status-expire-drop-down f6 dropdown details-reset details-overlay d-inline-block mr-2">
    <summary class="f5 btn-link link-gray-dark border px-2 py-1 rounded-1" aria-haspopup="true">
      <div class="js-user-status-expiration-interval-selected d-inline-block v-align-baseline">
        Never
      </div>
      <div class="dropdown-caret"></div>
    </summary>

    <ul class="dropdown-menu dropdown-menu-se pl-0 overflow-auto" style="width: 220px; max-height: 15.5em">
      <li>
        <button type="button" class="btn-link dropdown-item js-user-status-expire-button ws-normal" title="Never">
          <span class="d-inline-block text-bold mb-1">Never</span>
          <div class="f6 lh-condensed">Keep this status until you clear your status or edit your status.</div>
        </button>
      </li>
      <li class="dropdown-divider" role="none"></li>
        <li>
          <button type="button" class="btn-link dropdown-item ws-normal js-user-status-expire-button" title="in 30 minutes" value="2020-03-25T14:43:16+09:00">
            in 30 minutes
          </button>
        </li>
        <li>
          <button type="button" class="btn-link dropdown-item ws-normal js-user-status-expire-button" title="in 1 hour" value="2020-03-25T15:13:16+09:00">
            in 1 hour
          </button>
        </li>
        <li>
          <button type="button" class="btn-link dropdown-item ws-normal js-user-status-expire-button" title="in 4 hours" value="2020-03-25T18:13:16+09:00">
            in 4 hours
          </button>
        </li>
        <li>
          <button type="button" class="btn-link dropdown-item ws-normal js-user-status-expire-button" title="today" value="2020-03-25T23:59:59+09:00">
            today
          </button>
        </li>
        <li>
          <button type="button" class="btn-link dropdown-item ws-normal js-user-status-expire-button" title="this week" value="2020-03-29T23:59:59+09:00">
            this week
          </button>
        </li>
    </ul>
  </details>
  <input class="js-user-status-expiration-date-input" type="hidden" name="expires_at" value="">
</div>

          <include-fragment class="js-user-status-org-picker" data-url="/users/status/organizations"></include-fragment>
        </div>
        <div class="d-flex flex-items-center flex-justify-between p-3 border-top">
          <button type="submit" disabled class="width-full btn btn-primary mr-2 js-user-status-submit">
            Set status
          </button>
          <button type="button" disabled class="width-full js-clear-user-status-button btn ml-2 ">
            Clear status
          </button>
        </div>
</form>    </details-dialog>
  </details>
</div>

      </div>
      <div role="none" class="dropdown-divider"></div>

    <a role="menuitem" class="dropdown-item" href="/sakura68" data-ga-click="Header, go to profile, text:your profile">Your profile</a>

    <a role="menuitem" class="dropdown-item" href="/sakura68?tab=repositories" data-ga-click="Header, go to repositories, text:your repositories">Your repositories</a>

    <a role="menuitem" class="dropdown-item" href="/sakura68?tab=projects" data-ga-click="Header, go to projects, text:your projects">Your projects</a>

    <a role="menuitem" class="dropdown-item" href="/sakura68?tab=stars" data-ga-click="Header, go to starred repos, text:your stars">Your stars</a>
      <a role="menuitem" class="dropdown-item" href="https://gist.github.com/mine" data-ga-click="Header, your gists, text:your gists">Your gists</a>





    <div role="none" class="dropdown-divider"></div>
      
<div id="feature-enrollment-toggle" class="hide-sm hide-md feature-preview-details position-relative">
  <button
    type="button"
    class="dropdown-item btn-link"
    role="menuitem"
    data-feature-preview-trigger-url="/users/sakura68/feature_previews"
    data-feature-preview-close-details="{&quot;event_type&quot;:&quot;feature_preview.clicks.close_modal&quot;,&quot;payload&quot;:{&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}"
    data-feature-preview-close-hmac="70125ab3a318c552f9ccd64cef3391281a01b4a3ae51f183020188eeb20aff03"
    data-hydro-click="{&quot;event_type&quot;:&quot;feature_preview.clicks.open_modal&quot;,&quot;payload&quot;:{&quot;link_location&quot;:&quot;user_dropdown&quot;,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}"
    data-hydro-click-hmac="065c1c7e8a5caa075ca1cb69a9d80d07977095668d7c0b64cfc18263b8a79c2b"
  >
    Feature preview
  </button>
    <span class="feature-preview-indicator js-feature-preview-indicator" hidden></span>
</div>

    <a role="menuitem" class="dropdown-item" href="https://help.github.com" data-ga-click="Header, go to help, text:help">Help</a>
    <a role="menuitem" class="dropdown-item" href="/settings/profile" data-ga-click="Header, go to settings, icon:settings">Settings</a>
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="logout-form" action="/logout" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="iY12giKh4UIuc23o73i5cp59Mj7awUUwW2tLdWEEOSuV3NWWX2cc83Q7M7xGNUhJ+RVnq2k1HpoSuz/Kp8EOFw==" />
      
      <button type="submit" class="dropdown-item dropdown-signout" data-ga-click="Header, sign out, icon:logout" role="menuitem">
        Sign out
      </button>
      <input type="text" name="required_field_f405" hidden="hidden" class="form-control" /><input type="hidden" name="timestamp" value="1585113196494" class="form-control" /><input type="hidden" name="timestamp_secret" value="eafdb2c26db0438c1959b83fe5ba5e5a235c1e6c9e1df8d6493fc56b730519e7" class="form-control" />
</form>  </details-menu>
</details>

  </div>

</header>

      

  </div>

  <div id="start-of-content" class="show-on-focus"></div>


    <div id="js-flash-container">

</div>


      

  <include-fragment class="js-notification-shelf-include-fragment" data-base-src="https://github.com/notifications/beta/shelf"></include-fragment>




  <div class="application-main " data-commit-hovercards-enabled>
        <div itemscope itemtype="http://schema.org/SoftwareSourceCode" class="">
    <main id="js-repo-pjax-container" data-pjax-container >
      

  


      <div class="border-bottom shelf intro-shelf js-notice mb-0 pb-4">
  <div class="width-full container">
    <div class="width-full mx-auto shelf-content">
      <h2 class="shelf-title">Learn Git and GitHub without any code!</h2>
      <p class="shelf-lead">
          Using the Hello World guide, you’ll start a branch, write comments, and open a pull request.
      </p>
      <a class="btn btn-primary shelf-cta" target="_blank" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;READ_GUIDE&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="11dc3a7e0c193462e6867c4c01535d5c0de4c4dffdffc72a9bceb14ce6b6d26d" href="https://guides.github.com/activities/hello-world/">Read the guide</a>
    </div>
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="shelf-dismiss js-notice-dismiss" action="/dashboard/dismiss_bootcamp" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="delete" /><input type="hidden" name="authenticity_token" value="atg57JtkMCyCm7RL/ZKXo3Oo58Egh0yPahd+dqXMqG7Y6qV0SnrRHtQO7o0arStyVKOHb4d4p7AU4SNJ9bEKKg==" />
      <button name="button" type="submit" class="mr-1 close-button tooltipped tooltipped-w" aria-label="Hide this notice forever" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;DISMISS_BANNER&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="785469ef8ca2411eddf2a191e3300dcf398a3979c2d9855c7ad5f79e535291b8">
        <svg aria-label="Hide this notice forever" class="octicon octicon-x v-align-text-top" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
</button></form>  </div>
</div>



  









  <div class="pagehead repohead hx_repohead readability-menu bg-gray-light pb-0 pt-3">

    <div class="d-flex container-lg mb-4 px-3">

      <div class="flex-auto min-width-0 width-fit mr-3">
        <h1 class="public  d-flex flex-wrap flex-items-center break-word float-none ">
    <svg class="octicon octicon-repo" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
  <span class="author ml-1 flex-self-stretch" itemprop="author">
    <a class="url fn" rel="author" data-hovercard-type="organization" data-hovercard-url="/orgs/googlesamples/hovercard" href="/googlesamples">googlesamples</a>
  </span>
  <span class="path-divider flex-self-stretch">/</span>
  <strong itemprop="name" class="mr-2 flex-self-stretch">
    <a data-pjax="#js-repo-pjax-container" href="/googlesamples/unity-jar-resolver">unity-jar-resolver</a>
  </strong>
  
</h1>


      </div>

      <ul class="pagehead-actions flex-shrink-0 " >




  <li>
    
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form data-remote="true" class="clearfix js-social-form js-social-container" action="/notifications/subscribe" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="n07cVV57RbOc8hBCTcC3h3pFQW7fSfCWuh4hWoz8QQSBT3ErYDYhz760PXYvN0ORp9auZiyEOL83ggKQPcqcZQ==" />      <input type="hidden" name="repository_id" value="49642334">

      <details class="details-reset details-overlay select-menu float-left">
        <summary class="select-menu-button float-left btn btn-sm btn-with-count" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;WATCH_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="d7bc892a945b7b2d7c35da7d664ec71c6c9ab3fcd715d42f4222e8cc74e476b1" data-ga-click="Repository, click Watch settings, action:files#disambiguate">          <span data-menu-button>
              <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
              Watch
          </span>
</summary>        <details-menu
          class="select-menu-modal position-absolute mt-5"
          style="z-index: 99;">
          <div class="select-menu-header">
            <span class="select-menu-title">Notifications</span>
          </div>
          <div class="select-menu-list">
            <button type="submit" name="do" value="included" class="select-menu-item width-full" aria-checked="true" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Not watching</span>
                <span class="description">Be notified only when participating or @mentioned.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
                  Watch
                </span>
              </div>
            </button>

            <button type="submit" name="do" value="release_only" class="select-menu-item width-full" aria-checked="false" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Releases only</span>
                <span class="description">Be notified of new releases, and when participating or @mentioned.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
                  Unwatch releases
                </span>
              </div>
            </button>

            <button type="submit" name="do" value="subscribed" class="select-menu-item width-full" aria-checked="false" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Watching</span>
                <span class="description">Be notified of all conversations.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
                  Unwatch
                </span>
              </div>
            </button>

            <button type="submit" name="do" value="ignore" class="select-menu-item width-full" aria-checked="false" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Ignoring</span>
                <span class="description">Never be notified.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-mute v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 2.81v10.38c0 .67-.81 1-1.28.53L3 10H1c-.55 0-1-.45-1-1V7c0-.55.45-1 1-1h2l3.72-3.72C7.19 1.81 8 2.14 8 2.81zm7.53 3.22l-1.06-1.06-1.97 1.97-1.97-1.97-1.06 1.06L11.44 8 9.47 9.97l1.06 1.06 1.97-1.97 1.97 1.97 1.06-1.06L13.56 8l1.97-1.97z"/></svg>
                  Stop ignoring
                </span>
              </div>
            </button>
          </div>
        </details-menu>
      </details>
        <a class="social-count js-social-count"
          href="/googlesamples/unity-jar-resolver/watchers"
          aria-label="54 users are watching this repository">
          54
        </a>
</form>
  </li>

  <li>
      <div class="js-toggler-container js-social-container starring-container ">
    <form class="starred js-social-form" action="/googlesamples/unity-jar-resolver/unstar" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="AT9uzovUWarv1K6Z49RymF9J0H/hGFArTkQGoPkFb3uLmBgrZx+lwzNfBXS/FwgP47dScmlBkxRN5sFnzPMTdA==" />
      <input type="hidden" name="context" value="repository"></input>
      <button type="submit" class="btn btn-sm btn-with-count js-toggler-target" aria-label="Unstar this repository" title="Unstar googlesamples/unity-jar-resolver" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;UNSTAR_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="87bc95fec1ee8e37b4a7bf332b93ba719c85a0eed43015db87a400f9ee2b08bd" data-ga-click="Repository, click unstar button, action:files#disambiguate; text:Unstar">        <svg height="16" class="octicon octicon-star v-align-text-bottom" vertical_align="text_bottom" viewBox="0 0 14 16" version="1.1" width="14" aria-hidden="true"><path fill-rule="evenodd" d="M14 6l-4.9-.64L7 1 4.9 5.36 0 6l3.6 3.26L2.67 14 7 11.67 11.33 14l-.93-4.74L14 6z"/></svg>

        Unstar
</button>        <a class="social-count js-social-count" href="/googlesamples/unity-jar-resolver/stargazers"
           aria-label="430 users starred this repository">
           430
        </a>
</form>
    <form class="unstarred js-social-form" action="/googlesamples/unity-jar-resolver/star" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="q2KJMVrCNFXcv2nh9wbZblG+o0RjRQwPGANToVwtau4Gnm1q0jJjplp/3fosgAQ05VYj14buFVz/orzHYJXzdw==" />
      <input type="hidden" name="context" value="repository"></input>
      <button type="submit" class="btn btn-sm btn-with-count js-toggler-target" aria-label="Unstar this repository" title="Star googlesamples/unity-jar-resolver" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;STAR_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="e84aab734b730cc80ed407fb0bb0ef6b0f4a2854fc8b6d80220436d5529723d2" data-ga-click="Repository, click star button, action:files#disambiguate; text:Star">        <svg height="16" class="octicon octicon-star v-align-text-bottom" vertical_align="text_bottom" viewBox="0 0 14 16" version="1.1" width="14" aria-hidden="true"><path fill-rule="evenodd" d="M14 6l-4.9-.64L7 1 4.9 5.36 0 6l3.6 3.26L2.67 14 7 11.67 11.33 14l-.93-4.74L14 6z"/></svg>

        Star
</button>        <a class="social-count js-social-count" href="/googlesamples/unity-jar-resolver/stargazers"
           aria-label="430 users starred this repository">
          430
        </a>
</form>  </div>

  </li>

  <li>
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="btn-with-count" action="/googlesamples/unity-jar-resolver/fork" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="dcSPUYr6JRW67mEITPaqCNf9yPN+3Up1yYuWOWe5elgB5wSE8oTW0uZvdPWlWjzN1S7bklGWioCvq64t1v5fHQ==" />
            <button class="btn btn-sm btn-with-count" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;FORK_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="81fcbc078914cf6f6cf42e9bffedcf2eba3a9374d8a0f61aa5927dbd9fd19142" data-ga-click="Repository, show fork modal, action:files#disambiguate; text:Fork" type="submit" title="Fork your own copy of googlesamples/unity-jar-resolver to your account" aria-label="Fork your own copy of googlesamples/unity-jar-resolver to your account">              <svg class="octicon octicon-repo-forked v-align-text-bottom" viewBox="0 0 10 16" version="1.1" width="10" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 1a1.993 1.993 0 00-1 3.72V6L5 8 3 6V4.72A1.993 1.993 0 002 1a1.993 1.993 0 00-1 3.72V6.5l3 3v1.78A1.993 1.993 0 005 15a1.993 1.993 0 001-3.72V9.5l3-3V4.72A1.993 1.993 0 008 1zM2 4.2C1.34 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm3 10c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm3-10c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg>
              Fork
</button></form>
    <a href="/googlesamples/unity-jar-resolver/network/members" class="social-count"
       aria-label="159 users forked this repository">
      159
    </a>
  </li>
</ul>

    </div>
      
<nav class="hx_reponav reponav js-repo-nav js-sidenav-container-pjax clearfix container-lg px-3"
     itemscope
     itemtype="http://schema.org/BreadcrumbList"
    aria-label="Repository"
     data-pjax="#js-repo-pjax-container">

  <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
    <a class="js-selected-navigation-item selected reponav-item" itemprop="url" data-hotkey="g c" aria-current="page" data-selected-links="repo_source repo_downloads repo_commits repo_releases repo_tags repo_branches repo_packages /googlesamples/unity-jar-resolver" href="/googlesamples/unity-jar-resolver">
      <div class="d-inline"><svg class="octicon octicon-code" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M9.5 3L8 4.5 11.5 8 8 11.5 9.5 13 14 8 9.5 3zm-5 0L0 8l4.5 5L6 11.5 2.5 8 6 4.5 4.5 3z"/></svg></div>
      <span itemprop="name">Code</span>
      <meta itemprop="position" content="1">
</a>  </span>

    <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
      <a itemprop="url" data-hotkey="g i" class="js-selected-navigation-item reponav-item" data-selected-links="repo_issues repo_labels repo_milestones /googlesamples/unity-jar-resolver/issues" href="/googlesamples/unity-jar-resolver/issues">
        <div class="d-inline"><svg class="octicon octicon-issue-opened" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 011.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"/></svg></div>
        <span itemprop="name">Issues</span>
        <span class="Counter">28</span>
        <meta itemprop="position" content="2">
</a>    </span>

  <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
    <a data-hotkey="g p" data-skip-pjax="true" itemprop="url" class="js-selected-navigation-item reponav-item" data-selected-links="repo_pulls checks /googlesamples/unity-jar-resolver/pulls" href="/googlesamples/unity-jar-resolver/pulls">
      <div class="d-inline"><svg class="octicon octicon-git-pull-request" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M11 11.28V5c-.03-.78-.34-1.47-.94-2.06C9.46 2.35 8.78 2.03 8 2H7V0L4 3l3 3V4h1c.27.02.48.11.69.31.21.2.3.42.31.69v6.28A1.993 1.993 0 0010 15a1.993 1.993 0 001-3.72zm-1 2.92c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zM4 3c0-1.11-.89-2-2-2a1.993 1.993 0 00-1 3.72v6.56A1.993 1.993 0 002 15a1.993 1.993 0 001-3.72V4.72c.59-.34 1-.98 1-1.72zm-.8 10c0 .66-.55 1.2-1.2 1.2-.65 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2zM2 4.2C1.34 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg></div>
      <span itemprop="name">Pull requests</span>
      <span class="Counter">2</span>
      <meta itemprop="position" content="4">
</a>  </span>


    <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement" class="position-relative float-left">
      <a data-hotkey="g w" data-skip-pjax="true" class="js-selected-navigation-item reponav-item" data-selected-links="repo_actions /googlesamples/unity-jar-resolver/actions" href="/googlesamples/unity-jar-resolver/actions">
        <div class="d-inline"><svg class="octicon octicon-play" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M14 8A7 7 0 110 8a7 7 0 0114 0zm-8.223 3.482l4.599-3.066a.5.5 0 000-.832L5.777 4.518A.5.5 0 005 4.934v6.132a.5.5 0 00.777.416z"/></svg></div>
        Actions
</a>
    </span>

    <a data-hotkey="g b" class="js-selected-navigation-item reponav-item" data-selected-links="repo_projects new_repo_project repo_project /googlesamples/unity-jar-resolver/projects" href="/googlesamples/unity-jar-resolver/projects">
      <div class="d-inline"><svg class="octicon octicon-project" viewBox="0 0 15 16" version="1.1" width="15" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 00-1 1v14a1 1 0 001 1h13a1 1 0 001-1V1a1 1 0 00-1-1z"/></svg></div>
      Projects
      <span class="Counter">0</span>
</a>
    <a class="js-selected-navigation-item reponav-item" data-hotkey="g w" data-selected-links="repo_wiki /googlesamples/unity-jar-resolver/wiki" href="/googlesamples/unity-jar-resolver/wiki">
      <div class="d-inline"><svg class="octicon octicon-book" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M3 5h4v1H3V5zm0 3h4V7H3v1zm0 2h4V9H3v1zm11-5h-4v1h4V5zm0 2h-4v1h4V7zm0 2h-4v1h4V9zm2-6v9c0 .55-.45 1-1 1H9.5l-1 1-1-1H2c-.55 0-1-.45-1-1V3c0-.55.45-1 1-1h5.5l1 1 1-1H15c.55 0 1 .45 1 1zm-8 .5L7.5 3H2v9h6V3.5zm7-.5H9.5l-.5.5V12h6V3z"/></svg></div>
      Wiki
</a>
    <a data-skip-pjax="true" class="js-selected-navigation-item reponav-item" data-selected-links="security alerts policy token_scanning code_scanning /googlesamples/unity-jar-resolver/security/advisories" href="/googlesamples/unity-jar-resolver/security/advisories">
      <div class="d-inline"><svg class="octicon octicon-shield" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M0 2l7-2 7 2v6.02C14 12.69 8.69 16 7 16c-1.69 0-7-3.31-7-7.98V2zm1 .75L7 1l6 1.75v5.268C13 12.104 8.449 15 7 15c-1.449 0-6-2.896-6-6.982V2.75zm1 .75L7 2v12c-1.207 0-5-2.482-5-5.985V3.5z"/></svg></div>
      Security
</a>
    <a class="js-selected-navigation-item reponav-item" data-selected-links="repo_graphs repo_contributors dependency_graph dependabot_updates pulse people /googlesamples/unity-jar-resolver/pulse" href="/googlesamples/unity-jar-resolver/pulse">
      <div class="d-inline"><svg class="octicon octicon-graph" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M16 14v1H0V0h1v14h15zM5 13H3V8h2v5zm4 0H7V3h2v10zm4 0h-2V6h2v7z"/></svg></div>
      Insights
</a>

</nav>


  </div>


<div class="container-lg clearfix new-discussion-timeline  px-3">
  <div class="repository-content ">

    
    
  

      <div class="">  <div class="f4">
        <span class="text-gray-dark mr-2" itemprop="about">
          Unity plugin which resolves Android &amp; iOS dependencies and performs version management
        </span>
  </div>
</div>

    <div class="repository-topics-container mt-2 mb-3 js-topics-list-container">  <div class="list-topics-container f6">
      <a class="topic-tag topic-tag-link " href="/topics/unity-plugin" title="Topic: unity-plugin" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:unity-plugin">
  unity-plugin
</a>

      <a class="topic-tag topic-tag-link " href="/topics/unity" title="Topic: unity" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:unity">
  unity
</a>

      <a class="topic-tag topic-tag-link " href="/topics/android-resolver" title="Topic: android-resolver" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:android-resolver">
  android-resolver
</a>

      <a class="topic-tag topic-tag-link " href="/topics/ios-resolver" title="Topic: ios-resolver" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:ios-resolver">
  ios-resolver
</a>

      <a class="topic-tag topic-tag-link " href="/topics/jar-resolver" title="Topic: jar-resolver" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:jar-resolver">
  jar-resolver
</a>

      <a class="topic-tag topic-tag-link " href="/topics/android-dependency" title="Topic: android-dependency" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:android-dependency">
  android-dependency
</a>

      <a class="topic-tag topic-tag-link " href="/topics/cocoapods" title="Topic: cocoapods" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:cocoapods">
  cocoapods
</a>

      <a class="topic-tag topic-tag-link " href="/topics/unity-android" title="Topic: unity-android" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:unity-android">
  unity-android
</a>

      <a class="topic-tag topic-tag-link " href="/topics/unity-ios" title="Topic: unity-ios" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:unity-ios">
  unity-ios
</a>

      <a class="topic-tag topic-tag-link " href="/topics/unity-package-manager" title="Topic: unity-package-manager" data-ga-click="Topic, repository page" data-octo-click="topic_click" data-octo-dimensions="topic:unity-package-manager">
  unity-package-manager
</a>

  </div>
</div>



      <div class="overall-summary border-bottom-0 mb-0 rounded-bottom-0">
    <ul class="numbers-summary">
      <li class="commits">
        <a data-pjax href="/googlesamples/unity-jar-resolver/commits/master">
            <svg class="octicon octicon-git-commit" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M10.86 7c-.45-1.72-2-3-3.86-3-1.86 0-3.41 1.28-3.86 3H0v2h3.14c.45 1.72 2 3 3.86 3 1.86 0 3.41-1.28 3.86-3H14V7h-3.14zM7 10.2c-1.22 0-2.2-.98-2.2-2.2 0-1.22.98-2.2 2.2-2.2 1.22 0 2.2.98 2.2 2.2 0 1.22-.98 2.2-2.2 2.2z"/></svg>
            <span class="num text-emphasized">
              688
            </span>
            commits
        </a>
      </li>
      <li>
        <a data-pjax href="/googlesamples/unity-jar-resolver/branches">
          <svg class="octicon octicon-git-branch" viewBox="0 0 10 16" version="1.1" width="10" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M10 5c0-1.11-.89-2-2-2a1.993 1.993 0 00-1 3.72v.3c-.02.52-.23.98-.63 1.38-.4.4-.86.61-1.38.63-.83.02-1.48.16-2 .45V4.72a1.993 1.993 0 00-1-3.72C.88 1 0 1.89 0 3a2 2 0 001 1.72v6.56c-.59.35-1 .99-1 1.72 0 1.11.89 2 2 2 1.11 0 2-.89 2-2 0-.53-.2-1-.53-1.36.09-.06.48-.41.59-.47.25-.11.56-.17.94-.17 1.05-.05 1.95-.45 2.75-1.25S8.95 7.77 9 6.73h-.02C9.59 6.37 10 5.73 10 5zM2 1.8c.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2C1.35 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2zm0 12.41c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm6-8c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg>
          <span class="num text-emphasized">
            2
          </span>
          branches
        </a>
      </li>

        <li>
          <a data-pjax href="/googlesamples/unity-jar-resolver/packages" data-ga-click="Repository, packages click, location:repo overview">
            <svg class="octicon octicon-package" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M1 4.27v7.47c0 .45.3.84.75.97l6.5 1.73c.16.05.34.05.5 0l6.5-1.73c.45-.13.75-.52.75-.97V4.27c0-.45-.3-.84-.75-.97l-6.5-1.74a1.4 1.4 0 00-.5 0L1.75 3.3c-.45.13-.75.52-.75.97zm7 9.09l-6-1.59V5l6 1.61v6.75zM2 4l2.5-.67L11 5.06l-2.5.67L2 4zm13 7.77l-6 1.59V6.61l2-.55V8.5l2-.53V5.53L15 5v6.77zm-2-7.24L6.5 2.8l2-.53L15 4l-2 .53z"/></svg>
            <span class="num text-emphasized">
              0
            </span>
            packages
          </a>

        </li>

      <li>
        <a href="/googlesamples/unity-jar-resolver/releases">
          <svg class="octicon octicon-tag" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.73 1.73C7.26 1.26 6.62 1 5.96 1H3.5C2.13 1 1 2.13 1 3.5v2.47c0 .66.27 1.3.73 1.77l6.06 6.06c.39.39 1.02.39 1.41 0l4.59-4.59a.996.996 0 000-1.41L7.73 1.73zM2.38 7.09c-.31-.3-.47-.7-.47-1.13V3.5c0-.88.72-1.59 1.59-1.59h2.47c.42 0 .83.16 1.13.47l6.14 6.13-4.73 4.73-6.13-6.15zM3.01 3h2v2H3V3h.01z"/></svg>
          <span class="num text-emphasized">
            59
          </span>
          releases
        </a>
      </li>

        <li  data-contributors-hovercards-enabled >
            <a href="/googlesamples/unity-jar-resolver/graphs/contributors" data-hovercard-type="contributors" data-hovercard-url="/googlesamples/unity-jar-resolver/community_contributors/hovercard">
  <svg class="octicon octicon-organization" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M16 12.999c0 .439-.45 1-1 1H7.995c-.539 0-.994-.447-.995-.999H1c-.54 0-1-.561-1-1 0-2.634 3-4 3-4s.229-.409 0-1c-.841-.621-1.058-.59-1-3 .058-2.419 1.367-3 2.5-3s2.442.58 2.5 3c.058 2.41-.159 2.379-1 3-.229.59 0 1 0 1s1.549.711 2.42 2.088C9.196 9.369 10 8.999 10 8.999s.229-.409 0-1c-.841-.62-1.058-.59-1-3 .058-2.419 1.367-3 2.5-3s2.437.581 2.495 3c.059 2.41-.158 2.38-1 3-.229.59 0 1 0 1s3.005 1.366 3.005 4z"/></svg>
    <span class="num text-emphasized">
      40
    </span>
    contributors
</a>
        </li>

        <li>
          <a href="/googlesamples/unity-jar-resolver/blob/master/LICENSE">
            <svg class="octicon octicon-law" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7 4c-.83 0-1.5-.67-1.5-1.5S6.17 1 7 1s1.5.67 1.5 1.5S7.83 4 7 4zm7 6c0 1.11-.89 2-2 2h-1c-1.11 0-2-.89-2-2l2-4h-1c-.55 0-1-.45-1-1H8v8c.42 0 1 .45 1 1h1c.42 0 1 .45 1 1H3c0-.55.58-1 1-1h1c0-.55.58-1 1-1h.03L6 5H5c0 .55-.45 1-1 1H3l2 4c0 1.11-.89 2-2 2H2c-1.11 0-2-.89-2-2l2-4H1V5h3c0-.55.45-1 1-1h4c.55 0 1 .45 1 1h3v1h-1l2 4zM2.5 7L1 10h3L2.5 7zM13 10l-1.5-3-1.5 3h3z"/></svg>
              View license
          </a>
        </li>
    </ul>
  </div>

    <details class="details-reset">
      <summary title="Click for language details" data-ga-click="Repository, language bar stats toggle, location:repo overview">
        <div class="d-flex repository-lang-stats-graph">
            <span class="language-color" aria-label="C# 96.2%" style="width:96.2%; background-color:#178600;" itemprop="keywords">C#</span>
            <span class="language-color" aria-label="Python 3.2%" style="width:3.2%; background-color:#3572A5;" itemprop="keywords">Python</span>
            <span class="language-color" aria-label="Shell 0.6%" style="width:0.6%; background-color:#89e051;" itemprop="keywords">Shell</span>
        </div>
      </summary>
      <div class="repository-lang-stats">
        <ol class="repository-lang-stats-numbers">
          <li>
              <a href="/googlesamples/unity-jar-resolver/search?l=c%23"  data-ga-click="Repository, language stats search click, location:repo overview">
                <span class="color-block language-color" style="background-color:#178600;"></span>
                <span class="lang">C#</span>
                <span class="percent">96.2%</span>
              </a>
          </li>
          <li>
              <a href="/googlesamples/unity-jar-resolver/search?l=python"  data-ga-click="Repository, language stats search click, location:repo overview">
                <span class="color-block language-color" style="background-color:#3572A5;"></span>
                <span class="lang">Python</span>
                <span class="percent">3.2%</span>
              </a>
          </li>
          <li>
              <a href="/googlesamples/unity-jar-resolver/search?l=shell"  data-ga-click="Repository, language stats search click, location:repo overview">
                <span class="color-block language-color" style="background-color:#89e051;"></span>
                <span class="lang">Shell</span>
                <span class="percent">0.6%</span>
              </a>
          </li>
        </ol>
      </div>
    </details>





  <div class="mt-2">
    <include-fragment src="/googlesamples/unity-jar-resolver/show_partial?partial=tree%2Frecently_touched_branches_list"></include-fragment>
  </div>

<div class="file-navigation in-mid-page mb-2 d-flex flex-items-start">
  
<details class="details-reset details-overlay branch-select-menu " id="branch-select-menu">
  <summary class="btn css-truncate btn-sm"
           data-hotkey="w"
           title="Switch branches or tags">
    <i>Branch:</i>
    <span class="css-truncate-target" data-menu-button>master</span>
    <span class="dropdown-caret"></span>
  </summary>

  <details-menu class="SelectMenu SelectMenu--hasFilter" src="/googlesamples/unity-jar-resolver/refs/master?source_action=disambiguate&amp;source_controller=files" preload>
    <div class="SelectMenu-modal">
      <include-fragment class="SelectMenu-loading" aria-label="Menu is loading">
        <svg class="octicon octicon-octoface anim-pulse" height="32" viewBox="0 0 16 16" version="1.1" width="32" aria-hidden="true"><path fill-rule="evenodd" d="M14.7 5.34c.13-.32.55-1.59-.13-3.31 0 0-1.05-.33-3.44 1.3-1-.28-2.07-.32-3.13-.32s-2.13.04-3.13.32c-2.39-1.64-3.44-1.3-3.44-1.3-.68 1.72-.26 2.99-.13 3.31C.49 6.21 0 7.33 0 8.69 0 13.84 3.33 15 7.98 15S16 13.84 16 8.69c0-1.36-.49-2.48-1.3-3.35zM8 14.02c-3.3 0-5.98-.15-5.98-3.35 0-.76.38-1.48 1.02-2.07 1.07-.98 2.9-.46 4.96-.46 2.07 0 3.88-.52 4.96.46.65.59 1.02 1.3 1.02 2.07 0 3.19-2.68 3.35-5.98 3.35zM5.49 9.01c-.66 0-1.2.8-1.2 1.78s.54 1.79 1.2 1.79c.66 0 1.2-.8 1.2-1.79s-.54-1.78-1.2-1.78zm5.02 0c-.66 0-1.2.79-1.2 1.78s.54 1.79 1.2 1.79c.66 0 1.2-.8 1.2-1.79s-.53-1.78-1.2-1.78z"/></svg>
      </include-fragment>
    </div>
  </details-menu>
</details>


        <a class="btn btn-sm new-pull-request-btn ml-2" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;NEW_PULL_REQUEST_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="f003e60e5a9e570c2e246dd9a5cdc7bc62da456d51114acb3cbc3da80e032066" data-ga-click="Repository, new pull request, location:repo overview" data-pjax="true" href="/googlesamples/unity-jar-resolver/pull/new/master">New pull request</a>

  <div class="breadcrumb flex-auto">
    
  </div>

    <div class="BtnGroup ml-2">
        
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="BtnGroup-parent" action="/googlesamples/unity-jar-resolver/new/master" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="bS/31vN+9G3csK+tMvGf1lCxX2CMUMz8SLecNYSGRCnLhJtfY20nLGEV5tuc6TdNwt/TBIMKmg7B/AKarFzDmQ==" />
      <button class="btn btn-sm BtnGroup-item" type="submit" data-disable-with="Creating file…">
        Create new file
      </button>
</form>

        
    <a href="/googlesamples/unity-jar-resolver/upload/master" class="btn btn-sm BtnGroup-item">
      Upload files
    </a>


      <a class="btn btn-sm empty-icon float-right BtnGroup-item" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;target&quot;:&quot;FIND_FILE_BUTTON&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="949951f7bba7c0c482a68f8f88f59b54f9d46f1df925caaceae46486db74e088" data-ga-click="Repository, find file, location:repo overview" data-hotkey="t" data-pjax="true" href="/googlesamples/unity-jar-resolver/find/master">Find file</a>
    </div>


    <span class="d-flex">
        

        <details class="get-repo-select-menu js-get-repo-select-menu  position-relative details-overlay details-reset">
  <summary class="btn btn-sm ml-2 btn-primary" data-hydro-click="{&quot;event_type&quot;:&quot;repository.click&quot;,&quot;payload&quot;:{&quot;repository_id&quot;:49642334,&quot;target&quot;:&quot;CLONE_OR_DOWNLOAD_BUTTON&quot;,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="c702542d70a069c0b9999306467b82cbb52c0743557c6f243d1154361b66ced1">
      Clone or download
    <span class="dropdown-caret"></span>
</summary>  <div class="position-relative">
    <div class="get-repo-modal dropdown-menu dropdown-menu-sw pb-0 js-toggler-container  js-get-repo-modal">

      <div class="get-repo-modal-options">
          <div class="clone-options https-clone-options">
              <!-- '"` --><!-- </textarea></xmp> --></option></form><form data-remote="true" action="/users/set_protocol?protocol_selector=ssh&amp;protocol_type=clone" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="ZnRYim18FAqsyx2aeQp5QbBuXk4jyTwzsrhwXlQMaHkQdGnqs7BzUxCcmqEOixm7C+iBqEih2DlWql32UDeURQ==" /><button name="button" type="submit" data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;USE_SSH&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="8e833c677b870fcd94954d519892b03cdb728b1bdaa55daba5c3abd2af7e1d2a" class="btn-link btn-change-protocol js-toggler-target float-right">Use SSH</button></form>

            <h4 class="mb-1">
              Clone with HTTPS
              <a class="muted-link" href="https://help.github.com/articles/which-remote-url-should-i-use" target="_blank" title="Which remote URL should I use?">
                <svg class="octicon octicon-question" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M6 10h2v2H6v-2zm4-3.5C10 8.64 8 9 8 9H6c0-.55.45-1 1-1h.5c.28 0 .5-.22.5-.5v-1c0-.28-.22-.5-.5-.5h-1c-.28 0-.5.22-.5.5V7H4c0-1.5 1.5-3 3-3s3 1 3 2.5zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 011.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z"/></svg>
              </a>
            </h4>
            <p class="mb-2 get-repo-decription-text">
              Use Git or checkout with SVN using the web URL.
            </p>

            <div class="input-group">
  <input type="text" class="form-control input-monospace input-sm" data-autoselect value="https://github.com/googlesamples/unity-jar-resolver.git" aria-label="Clone this repository at https://github.com/googlesamples/unity-jar-resolver.git" readonly>
  <div class="input-group-button">
    <clipboard-copy value="https://github.com/googlesamples/unity-jar-resolver.git" aria-label="Copy to clipboard" class="btn btn-sm" data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;COPY_URL&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="b3e86f32f5fea24772e98e04acf80ea2c4d0b2e5e91e801750325fcdc757d10d"><svg class="octicon octicon-clippy" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M2 13h4v1H2v-1zm5-6H2v1h5V7zm2 3V8l-3 3 3 3v-2h5v-2H9zM4.5 9H2v1h2.5V9zM2 12h2.5v-1H2v1zm9 1h1v2c-.02.28-.11.52-.3.7-.19.18-.42.28-.7.3H1c-.55 0-1-.45-1-1V4c0-.55.45-1 1-1h3c0-1.11.89-2 2-2 1.11 0 2 .89 2 2h3c.55 0 1 .45 1 1v5h-1V6H1v9h10v-2zM2 5h8c0-.55-.45-1-1-1H8c-.55 0-1-.45-1-1s-.45-1-1-1-1 .45-1 1-.45 1-1 1H3c-.55 0-1 .45-1 1z"/></svg></clipboard-copy>
  </div>
</div>

          </div>

          <div class="clone-options ssh-clone-options">
              <!-- '"` --><!-- </textarea></xmp> --></option></form><form data-remote="true" action="/users/set_protocol?protocol_selector=https&amp;protocol_type=clone" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="aYFWq3tz8n/raZE0TiHTrg4bRuLdURCaFrZ/ea/q7FcfgWfLpb+VJlc+Fg85oLNUtZ2ZBLY59JDypFLRq9EQaw==" /><button name="button" type="submit" data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;USE_HTTPS&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="03d86675f86782d82db1821e56f31fbecf533c1f4ac0b9674ee3eeeb749b224f" class="btn-link btn-change-protocol js-toggler-target float-right">Use HTTPS</button></form>
              <h4 class="mb-1">
                Clone with SSH
                <a class="muted-link" href="https://help.github.com/articles/which-remote-url-should-i-use" target="_blank" title="Which remote URL should I use?">
                  <svg class="octicon octicon-question" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M6 10h2v2H6v-2zm4-3.5C10 8.64 8 9 8 9H6c0-.55.45-1 1-1h.5c.28 0 .5-.22.5-.5v-1c0-.28-.22-.5-.5-.5h-1c-.28 0-.5.22-.5.5V7H4c0-1.5 1.5-3 3-3s3 1 3 2.5zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 011.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z"/></svg>
                </a>
              </h4>

                <div class="flash flash-warn my-3">
                  You don't have any public SSH keys in your GitHub account.
                  You can <a href="/settings/ssh/new">add a new public key</a>, or try cloning this repository via <button class="btn-link js-toggler-target">HTTPS.</button>
                </div>

              <p class="mb-2 get-repo-decription-text">
                  Use a password protected SSH key.
              </p>

              <div class="input-group">
  <input type="text" class="form-control input-monospace input-sm" data-autoselect value="git@github.com:googlesamples/unity-jar-resolver.git" aria-label="Clone this repository at git@github.com:googlesamples/unity-jar-resolver.git" readonly>
  <div class="input-group-button">
    <clipboard-copy value="git@github.com:googlesamples/unity-jar-resolver.git" aria-label="Copy to clipboard" class="btn btn-sm" data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;COPY_URL&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="b3e86f32f5fea24772e98e04acf80ea2c4d0b2e5e91e801750325fcdc757d10d"><svg class="octicon octicon-clippy" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M2 13h4v1H2v-1zm5-6H2v1h5V7zm2 3V8l-3 3 3 3v-2h5v-2H9zM4.5 9H2v1h2.5V9zM2 12h2.5v-1H2v1zm9 1h1v2c-.02.28-.11.52-.3.7-.19.18-.42.28-.7.3H1c-.55 0-1-.45-1-1V4c0-.55.45-1 1-1h3c0-1.11.89-2 2-2 1.11 0 2 .89 2 2h3c.55 0 1 .45 1 1v5h-1V6H1v9h10v-2zM2 5h8c0-.55-.45-1-1-1H8c-.55 0-1-.45-1-1s-.45-1-1-1-1 .45-1 1-.45 1-1 1H3c-.55 0-1 .45-1 1z"/></svg></clipboard-copy>
  </div>
</div>

          </div>
        <div class="mt-2 d-flex">
          <a class="btn btn-outline get-repo-btn tooltipped tooltipped-s tooltipped-multiline js-remove-unless-platform js-get-repo" aria-label="Clone googlesamples/unity-jar-resolver to your computer and use it in GitHub Desktop." data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;OPEN_IN_DESKTOP&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="7b64b73eb4dd82a324c4b538301ab1a5ad41f2937e0ce15bb4185615a1531a92" data-platforms="windows,mac" href="x-github-client://openRepo/https://github.com/googlesamples/unity-jar-resolver">Open in Desktop</a>

<a class="flex-1 btn btn-outline get-repo-btn " rel="nofollow" data-hydro-click="{&quot;event_type&quot;:&quot;clone_or_download.click&quot;,&quot;payload&quot;:{&quot;feature_clicked&quot;:&quot;DOWNLOAD_ZIP&quot;,&quot;git_repository_type&quot;:&quot;REPOSITORY&quot;,&quot;repository_id&quot;:49642334,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}" data-hydro-click-hmac="c42a23c8a67831c64622e182957767fe2cf7888623c3933a0e86e50c3a29a573" data-ga-click="Repository, download zip, location:repo overview" href="/googlesamples/unity-jar-resolver/archive/master.zip">Download ZIP</a>

        </div>
      </div>


      <div class="js-modal-download-mac py-2 px-3 d-none">
        <h4 class="lh-condensed mb-3">Launching GitHub Desktop<span class="AnimatedEllipsis"></span></h4>
        <p class="text-gray">If nothing happens, <a href="https://desktop.github.com/">download GitHub Desktop</a> and try again.</p>
        <p><button class="btn-link js-get-repo-modal-download-back">Go back</button></p>
      </div>

      <div class="js-modal-download-windows py-2 px-3 d-none">
        <h4 class="lh-condensed mb-3">Launching GitHub Desktop<span class="AnimatedEllipsis"></span></h4>
        <p class="text-gray">If nothing happens, <a href="https://desktop.github.com/">download GitHub Desktop</a> and try again.</p>
        <p><button class="btn-link js-get-repo-modal-download-back">Go back</button></p>
      </div>

      <div class="js-modal-download-xcode py-2 px-3 d-none">
        <h4 class="lh-condensed mb-3">Launching Xcode<span class="AnimatedEllipsis"></span></h4>
        <p class="text-gray">If nothing happens, <a href="https://developer.apple.com/xcode/">download Xcode</a> and try again.</p>
        <p><button class="btn-link js-get-repo-modal-download-back">Go back</button></p>
      </div>

      <div class="js-modal-download-visual-studio py-2 px-3 d-none">
        <h4 class="lh-condensed mb-3">Launching Visual Studio<span class="AnimatedEllipsis"></span></h4>
        <p class="text-gray">If nothing happens, <a href="https://visualstudio.github.com/">download the GitHub extension for Visual Studio</a> and try again.</p>
        <p><button class="btn-link js-get-repo-modal-download-back">Go back</button></p>
      </div>

    </div>
  </div>
</details>


    </span>

    
</div>




<div class="Box mb-3 Box--condensed">
  <div class="Box-header Box-header--blue position-relative "
    style="margin-bottom:-1px;">
    <h2 class="sr-only">Latest commit</h2>
    <div class="commit-tease js-details-container Details d-flex rounded-top-1 flex-auto" data-issue-and-pr-hovercards-enabled>
        
    
<div class="AvatarStack flex-self-start ">
  <div class="AvatarStack-body" aria-label="stewartmiles">
        <a class="avatar" data-skip-pjax="true" data-hovercard-type="user" data-hovercard-url="/users/stewartmiles/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/stewartmiles">
          <img height="20" width="20" alt="@stewartmiles" src="https://avatars3.githubusercontent.com/u/6042948?s=60&amp;u=c16baaf2d763fdbd5e13d84a059d37104f9e2b0a&amp;v=4" />
</a>  </div>
</div>

    <div class="flex-auto f6 mr-3">
      
      <a href="/googlesamples/unity-jar-resolver/commits?author=stewartmiles"
     class="commit-author tooltipped tooltipped-s user-mention"
     aria-label="View all commits by stewartmiles">stewartmiles</a>


  


        <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="message text-inherit" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
          <span class="hidden-text-expander inline">
            <button type="button" class="ellipsis-expander js-details-target" aria-expanded="false">&hellip;</button>
          </span>

        <div class="commit-desc"><pre class="text-small">-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48</pre></div>
    </div>
    <div class="no-wrap d-flex flex-self-start flex-items-baseline">
        <span class="mr-2 flex-self-center">
          <include-fragment accept="text/fragment+html" src="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26/rollup" class="d-inline"></include-fragment>
        </span>
      <span class="mr-1">Latest commit</span>
      <a class="commit-tease-sha mr-1" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26" data-pjax>
        7675ea7
      </a>
      <span itemprop="dateModified"><relative-time datetime="2020-03-23T15:48:57Z" class="no-wrap">Mar 23, 2020</relative-time></span>
    </div>
    </div>
  </div>
  <h2 id="files"  class="sr-only">Files</h2>
  


    <a class="d-none js-permalink-shortcut" data-hotkey="y" href="/googlesamples/unity-jar-resolver/tree/7675ea739464b75573e2f0e6f98d51500fbbce26">Permalink</a>

    <table class="files js-navigation-container js-active-navigation-container " data-pjax>
      <thead>
        <tr>
          <th><span class="sr-only">Type</span></th>
          <th><span class="sr-only">Name</span></th>
          <th><span class="sr-only">Latest commit message</span></th>
          <th><span class="sr-only">Commit time</span></th>
        </tr>
      </thead>


      <tbody>
        <tr class="warning include-fragment-error">
          <td class="icon"><svg class="octicon octicon-alert" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 000 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 00.01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"/></svg></td>
          <td class="content" colspan="3">Failed to load latest commit information.</td>
        </tr>

          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="56197b3fc60f4850ae72270fe966ac92-4e07d7b5347272ee460afdbede0c671402c3e33a" href="/googlesamples/unity-jar-resolver/tree/master/.github/ISSUE_TEMPLATE"><span class="text-gray-light">.github/</span>ISSUE_TEMPLATE</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Rename PSR to External Dependency Manager

Bug: 150886091
Change-Id: Icc556d13bafe06b081e64d48dba03a9baecffdd0" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/5b4149310837d2651bebd426c4a7baf0acfe8580">Rename PSR to External Dependency Manager</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-06T16:39:44Z" class="no-wrap">Mar 7, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="b45bd5371c0b11060f22cae1abd737a5-f7cfb12ac96a245b2b2f9b552ab652b5268fecf8" href="/googlesamples/unity-jar-resolver/tree/master/exploded/Assets"><span class="text-gray-light">exploded/</span>Assets</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="2708aa26a268ce43e59f57fa8a59d9fb-43a8884b36c63e93f1e7dddf4bcfc6788b951471" href="/googlesamples/unity-jar-resolver/tree/master/gradle/wrapper"><span class="text-gray-light">gradle/</span>wrapper</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Update the Gradle wrapper to 5.1.1

This is required to use Kotlin plugins like the Jetifier with
Gradle&#39;s embedded Kotlin runtime.

https://github.com/googlesamples/unity-jar-resolver/issues/145
Bug: 113575309
Change-Id: I4d11d30d7944fb95a2c1c2f19f0762d55eda0024" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/44da56e883cf48501949eed32735ac7872e7f420">Update the Gradle wrapper to 5.1.1</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2019-06-13T18:22:22Z" class="no-wrap">Jun 14, 2019</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="85c03122f6f558b9207ffdc01824635d-ed314cb41c6c76b899f709e10273ed83be5c4e71" href="/googlesamples/unity-jar-resolver/tree/master/maven-indexes/dl.google.com/dl/android/maven2"><span class="text-gray-light">maven-indexes/dl.google.com/dl/android/</span>maven2</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.123

* All components: Source control integration for project settings.
* Android Resolver: Removed AAR cache as it now makes little difference to
  incremental resolution performance.
* Android Resolver: Improved embedded resource management so that embedded
  resources should upgrade when the plugin is updated without restarting
  the Unity editor.
* Version Handler: Fixed InvokeMethod() and InvokeStaticMethod() when calling
  methods that have interface typed arguments.

Change-Id: If0dee015bd7b21d8a1ce59ede6480edc89c6b921" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/43d700803aa9e2680d9254ea706a3b6b6c2bc1da">Version 1.2.123</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2019-07-23T21:20:00Z" class="no-wrap">Jul 24, 2019</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="b38df4f8f396eb07c023b8edc18fe555-4f93021c5a36604fbc37b514478b0b498fb0fa30" href="/googlesamples/unity-jar-resolver/tree/master/plugin/Assets"><span class="text-gray-light">plugin/</span>Assets</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="This path skips through empty directories" id="b0c33187b824f9d50f2b391e4022dcc6-6fc76efc49b60f725e4bf3fda59e574d895f2e12" href="/googlesamples/unity-jar-resolver/tree/master/sample/Assets/ExternalDependencyManager/Editor"><span class="text-gray-light">sample/Assets/ExternalDependencyManager/</span>Editor</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Revert &quot;Add ability to set the desired target from Dependencies file&quot;

This reverts commit 16ce8d0b886412308e4e9362e3fd1718e8e5bee3.
This is causing issues in Unity 2019.
1. GetXcodeTargetGuids() throws exception due to &quot;Unity-iPhone&quot; target being deprecated.
2. Generating &quot;Unity-iPhone&quot; target in Podfile with no pod by default.

Bug: 152165846
Change-Id: I188ebf01020380bc962ffb28aebf31a56ca7c397" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/df59a8232b602a1368b73b2a2a4794caada1a7a1">Revert "Add ability to set the desired target from Dependencies file"</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:37:32Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="directory" class="octicon octicon-file-directory" viewBox="0 0 14 16" version="1.1" width="14" height="16" role="img"><path fill-rule="evenodd" d="M13 4H7V3c0-.66-.31-1-1-1H1c-.55 0-1 .45-1 1v10c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V5c0-.55-.45-1-1-1zM6 4H1V3h5v1z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="source" id="36cd38f49b9afa08222c0dc9ebfe35eb-0c3a7160d79257a0acfd7819457f819bd77b6601" href="/googlesamples/unity-jar-resolver/tree/master/source">source</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title=".gitignore" id="a084b794bc0759e7a6b77810e01874f2-8deb894df8ca45e26a89600c8acc74944a875585" href="/googlesamples/unity-jar-resolver/blob/master/.gitignore">.gitignore</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Add Unity Asset Store Uploader python CLI.

Change-Id: Iaa5d95bfed24b2dc4aeaf88c9b07e566736bbef5" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/3e237404ca4c7ce2df2f9c46fcc545a05a40bfc9">Add Unity Asset Store Uploader python CLI.</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2019-07-08T18:36:38Z" class="no-wrap">Jul 9, 2019</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="CHANGELOG.md" id="4ac32a78649ca5bdd8e0ba38b7006a1e-ef800c4928563c613eef8ed8bfec2091fd28cbf8" href="/googlesamples/unity-jar-resolver/blob/master/CHANGELOG.md">CHANGELOG.md</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="CONTRIBUTING.md" id="6a3371457528722a734f3c51d9238c13-792e013173f8e4b6e4181b42878769e6411f3a77" href="/googlesamples/unity-jar-resolver/blob/master/CONTRIBUTING.md">CONTRIBUTING.md</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Adding CONTRIBUTING.md

Change-Id: I7f707c2dd27416ed0a23d0d8776e8f3311738259" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/579ee0e4fd70bbc4b75600e6ffe6d92cf7f0dd34">Adding CONTRIBUTING.md</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2015-12-08T23:50:38Z" class="no-wrap">Dec 9, 2015</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="LICENSE" id="9879d6db96fd29134fc802214163b95a-6258cc47e90a1f92ba1766e0fc31c1c1dc48bb6f" itemprop="license" href="/googlesamples/unity-jar-resolver/blob/master/LICENSE">LICENSE</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Add UnityPackageManagerResolver to EDM4U

UnityPackageManagerResolver helps to transition to manage packages
through Unity Package Manager, from Unity 2018.4.

- Prompts to add Game Package Registry by Google to Unity project.
  This allows the users to discover and manage packages from
  Google through Unity Package Manager.
- Added a setting window under &quot;Assets &gt; External Dependency Manager &gt;
  Unity Package Manager Resolver&quot; to add and remove registries.

Bug: 150471207
Change-Id: Ia2f1b497a412b850d561d62977808b09b7c1f5fc" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/c43deb59d4ac17df2ef2495d60bacecceab3dd3d">Add UnityPackageManagerResolver to EDM4U</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-10T21:50:50Z" class="no-wrap">Mar 11, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="README.md" id="04c6e90faac2675aa89e2176d2eec7d8-a30630c95f1efd5e9290f7ef0ee465c3aa279183" href="/googlesamples/unity-jar-resolver/blob/master/README.md">README.md</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.139

* Added documentation to the built plugin.

Change-Id: I159a4f45389fde20c58e7d284f957acdfafa7fa2" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/1ae4b0ef35ebcc3715f57f2ac9497a35f1dd49ae">Version 1.2.139</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-19T00:32:30Z" class="no-wrap">Mar 19, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="build.gradle" id="c197962302397baf3a4cc36463dce5ea-6db21a711ebe73bdf9c85390db264439a74b5ef7" href="/googlesamples/unity-jar-resolver/blob/master/build.gradle">build.gradle</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="external-dependency-manager-1.2.144.unitypackage" id="c101c8e9d4be8dfdde0e250837295544-a940d314ff28b6c4ddb71092ba481e88cfb80259" href="/googlesamples/unity-jar-resolver/blob/master/external-dependency-manager-1.2.144.unitypackage">external-dependency-manager-1.2.144.unitypackage</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="external-dependency-manager-latest.unitypackage" id="841da8a18cbaa0b2b8e4b0faf717046d-a940d314ff28b6c4ddb71092ba481e88cfb80259" href="/googlesamples/unity-jar-resolver/blob/master/external-dependency-manager-latest.unitypackage">external-dependency-manager-latest.unitypackage</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Version 1.2.144

-- Changed
* iOS Resolver: Removed the ability to configure the Xcode target
  a Cocoapod is added to.

-- Bug Fixes
* iOS Resolver: Reverted support for adding Cocoapods to multiple
  targets as it caused a regression (exception thrown during
  post-build step) in some versions of Unity.

Bug: 152165846
Change-Id: I9c46c4b223ba9edf43834837c03e4728d5026b48" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/7675ea739464b75573e2f0e6f98d51500fbbce26">Version 1.2.144</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2020-03-23T15:52:34Z" class="no-wrap">Mar 24, 2020</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="gradlew" id="36ffbd2ea7085cf19547193a7faf30c8-27309d92314c57d6e442529f1362bafc8376feae" href="/googlesamples/unity-jar-resolver/blob/master/gradlew">gradlew</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Upgraded Android Resolver to gradle 4.10 and fixed some issues.

* Upgraded Android Resolver build and library downloader to Gradle 4.10
  using `./gradlew wrapper --gradle-version 4.10`.
* Added task to update the gradle wrapper in the Android Resolver.
* Fixed support for dependency resolution when a transitive dependency is
  upgraded to a new version correctly by Gradle.
* Updated test data for download_artifacts_test to specify valid version
  expressions for Gradle 4.x (Gradle 2.x was fine with them).
* Improved reporting of conflicts by download_artifacts.gradle and increased
  verbosity of output by default.

Tested: All integration tests are passing.
Bug: 113343608
Change-Id: I8c5f43a9d251861cec3b0e80e1eb15fd71cd089b" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/fff89b8e0ce61f7af37561ba95d74ccb8416c3c4">Upgraded Android Resolver to gradle 4.10 and fixed some issues.</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2018-08-29T22:06:38Z" class="no-wrap">Aug 30, 2018</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="gradlew.bat" id="c9b677a57d25a366595a35b2230d0502-f6d5974e72fdac64fbae9aeb32c28cb8b69a92d5" href="/googlesamples/unity-jar-resolver/blob/master/gradlew.bat">gradlew.bat</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Upgraded Android Resolver to gradle 4.10 and fixed some issues.

* Upgraded Android Resolver build and library downloader to Gradle 4.10
  using `./gradlew wrapper --gradle-version 4.10`.
* Added task to update the gradle wrapper in the Android Resolver.
* Fixed support for dependency resolution when a transitive dependency is
  upgraded to a new version correctly by Gradle.
* Updated test data for download_artifacts_test to specify valid version
  expressions for Gradle 4.x (Gradle 2.x was fine with them).
* Improved reporting of conflicts by download_artifacts.gradle and increased
  verbosity of output by default.

Tested: All integration tests are passing.
Bug: 113343608
Change-Id: I8c5f43a9d251861cec3b0e80e1eb15fd71cd089b" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/fff89b8e0ce61f7af37561ba95d74ccb8416c3c4">Upgraded Android Resolver to gradle 4.10 and fixed some issues.</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2018-08-29T22:06:38Z" class="no-wrap">Aug 30, 2018</time-ago></span>
            </td>
          </tr>
          <tr class="js-navigation-item">
            <td class="icon">
              <svg aria-label="file" class="octicon octicon-file" viewBox="0 0 12 16" version="1.1" width="12" height="16" role="img"><path fill-rule="evenodd" d="M6 5H2V4h4v1zM2 8h7V7H2v1zm0 2h7V9H2v1zm0 2h7v-1H2v1zm10-7.5V14c0 .55-.45 1-1 1H1c-.55 0-1-.45-1-1V2c0-.55.45-1 1-1h7.5L12 4.5zM11 5L8 2H1v12h10V5z"/></svg>
              <img width="16" height="16" class="spinner" alt="" src="https://github.githubassets.com/images/spinners/octocat-spinner-32.gif" />
            </td>
            <td class="content">
              <span class="css-truncate css-truncate-target"><a class="js-navigation-open " title="settings.gradle" id="88b7c47e47b8ee65263b6784b86fa0a7-41cb76577865c199d28c2f42032a16b2b9b42900" href="/googlesamples/unity-jar-resolver/blob/master/settings.gradle">settings.gradle</a></span>
            </td>
            <td class="message">
              <span class="css-truncate css-truncate-target">
                    <a data-pjax="true" title="Fixed Android Resolver when run in a Gradle project.

If a parent directory containing a Unity project also contained
a settings.gradle (i.e Gradle project) the Android Resolver would
fail with:

PlayServicesResolver.scripts.download_artifacts.gradle
is not part of the build defined by settings file...

This fixes the issue by creating a Gradle project in the temporary
directory used by the Android Resolver to run the
download_artifacts.gradle script.

Fixes #278

Bug: 141749513
Change-Id: Ic6394b7bd0e7a4f9a3ffde33e1390a2a2d437a22" class="link-gray" href="/googlesamples/unity-jar-resolver/commit/c7cd4e56ddfbd893beb651cf1226d29123dd3c06">Fixed Android Resolver when run in a Gradle project.</a>
              </span>
            </td>
            <td class="age">
              <span class="css-truncate css-truncate-target"><time-ago datetime="2019-09-27T21:55:33Z" class="no-wrap">Sep 28, 2019</time-ago></span>
            </td>
          </tr>
      </tbody>
    </table>




</div>

  <div id="readme" class="Box md js-code-block-container Box--condensed">
    <div class="Box-header d-flex flex-items-center flex-justify-between ">
      <h2 class="Box-title pr-3">
        <svg class="octicon octicon-book" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M3 5h4v1H3V5zm0 3h4V7H3v1zm0 2h4V9H3v1zm11-5h-4v1h4V5zm0 2h-4v1h4V7zm0 2h-4v1h4V9zm2-6v9c0 .55-.45 1-1 1H9.5l-1 1-1-1H2c-.55 0-1-.45-1-1V3c0-.55.45-1 1-1h5.5l1 1 1-1H15c.55 0 1 .45 1 1zm-8 .5L7.5 3H2v9h6V3.5zm7-.5H9.5l-.5.5V12h6V3z"/></svg>
        README.md
      </h2>
    </div>
        <div class="Popover anim-scale-in js-tagsearch-popover"
     hidden
     data-tagsearch-url="/googlesamples/unity-jar-resolver/find-symbols"
     data-tagsearch-ref="master"
     data-tagsearch-path="README.md"
     data-tagsearch-lang="Markdown"
     data-hydro-click="{&quot;event_type&quot;:&quot;code_navigation.click_on_symbol&quot;,&quot;payload&quot;:{&quot;action&quot;:&quot;click_on_symbol&quot;,&quot;repository_id&quot;:49642334,&quot;ref&quot;:&quot;master&quot;,&quot;language&quot;:&quot;Markdown&quot;,&quot;originating_url&quot;:&quot;https://github.com/googlesamples/unity-jar-resolver&quot;,&quot;user_id&quot;:62635306}}"
     data-hydro-click-hmac="45175de48f8f10b750335680a18ee77027d94be5215a1b646206bcaa481d6a88">
  <div class="Popover-message Popover-message--large Popover-message--top-left TagsearchPopover mt-1 mb-4 mx-auto Box box-shadow-large">
    <div class="TagsearchPopover-content js-tagsearch-popover-content overflow-auto" style="will-change:transform;">
    </div>
  </div>
</div>

      <div class="Box-body p-5">
        <article class="markdown-body entry-content" itemprop="text"><h1><a id="user-content-external-dependency-manager-for-unity" class="anchor" aria-hidden="true" href="#external-dependency-manager-for-unity"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>External Dependency Manager for Unity</h1>
<h1><a id="user-content-overview" class="anchor" aria-hidden="true" href="#overview"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Overview</h1>
<p>The External Dependency Manager for Unity (EDM4U)
(formerly Play Services Resolver / Jar Resolver) is intended to be used by any
Unity plugin that requires:</p>
<ul>
<li>Android specific libraries (e.g
<a href="https://developer.android.com/studio/projects/android-library.html" rel="nofollow">AARs</a>).</li>
<li>iOS <a href="https://cocoapods.org/" rel="nofollow">CocoaPods</a>.</li>
<li>Version management of transitive dependencies.</li>
<li>Management of Unity Package Manager Registries.</li>
</ul>
<p>Updated releases are available on
<a href="https://github.com/googlesamples/unity-jar-resolver">GitHub</a></p>
<h1><a id="user-content-background" class="anchor" aria-hidden="true" href="#background"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Background</h1>
<p>Many Unity plugins have dependencies upon Android specific libraries, iOS
CocoaPods, and sometimes have transitive dependencies upon other Unity plugins.
This causes the following problems:</p>
<ul>
<li>Integrating platform specific (e.g Android and iOS) libraries within a
Unity project can be complex and a burden on a Unity plugin maintainer.</li>
<li>The process of resolving conflicting dependencies on platform specific
libraries is pushed to the developer attempting to use a Unity plugin.
The developer trying to use you plugin is very likely to give up when
faced with Android or iOS specific build errors.</li>
<li>The process of resolving conflicting Unity plugins (due to shared Unity
plugin components) is pushed to the developer attempting to use your Unity
plugin. In an effort to resolve conflicts, the developer will very likely
attempt to resolve problems by deleting random files in your plugin,
report bugs when that doesn't work and finally give up.</li>
</ul>
<p>EDM provides solutions for each of these problems.</p>
<h2><a id="user-content-android-dependency-management" class="anchor" aria-hidden="true" href="#android-dependency-management"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Android Dependency Management</h2>
<p>The <em>Android Resolver</em> component of this plugin will download and integrate
Android library dependencies and handle any conflicts between plugins that share
the same dependencies.</p>
<p>Without the Android Resolver, typically Unity plugins bundle their AAR and
JAR dependencies, e.g. a Unity plugin <code>SomePlugin</code> that requires the Google
Play Games Android library would redistribute the library and its transitive
dependencies in the folder <code>SomePlugin/Android/</code>.  When a user imports
<code>SomeOtherPlugin</code> that includes the same libraries (potentially at different
versions) in <code>SomeOtherPlugin/Android/</code>, the developer using <code>SomePlugin</code> and
<code>SomeOtherPlugin</code> will see an error when building for Android that can be hard
to interpret.</p>
<p>Using the Android Resolver to manage Android library dependencies:</p>
<ul>
<li>Solves Android library conflicts between plugins.</li>
<li>Handles all of the various processing steps required to use Android
libraries (AARs, JARs) in Unity 4.x and above projects.  Almost all
versions of Unity have - at best - partial support for AARs.</li>
<li>(Experimental) Supports minification of included Java components without
exporting a project.</li>
</ul>
<h2><a id="user-content-ios-dependency-management" class="anchor" aria-hidden="true" href="#ios-dependency-management"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>iOS Dependency Management</h2>
<p>The <em>iOS Resolver</em> component of this plugin integrates with
<a href="https://cocoapods.org/" rel="nofollow">CocoaPods</a> to download and integrate iOS libraries
and frameworks into the Xcode project Unity generates when building for iOS.
Using CocoaPods allows multiple plugins to utilize shared components without
forcing developers to fix either duplicate or incompatible versions of
libraries included through multiple Unity plugins in their project.</p>
<h2><a id="user-content-unity-package-manager-registry-setup" class="anchor" aria-hidden="true" href="#unity-package-manager-registry-setup"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Unity Package Manager Registry Setup</h2>
<p>The <a href="https://docs.unity3d.com/Manual/Packages.html" rel="nofollow">Unity Package Manager</a>
(UPM) makes use of <a href="https://www.npmjs.com/" rel="nofollow">NPM</a> registry servers for package
hosting and provides ways to discover, install, upgrade and uninstall packages.
This makes it easier for developers to manage plugins within their projects.</p>
<p>However, installing additional package registries requires a few manual steps
that can potentially be error prone.  The <em>Unity Package Manager Resolver</em>
component of this plugin integrates with
<a href="https://docs.unity3d.com/Manual/Packages.html" rel="nofollow">UPM</a> to provide a way to
auto-install UPM package registries when a <code>.unitypackage</code> is installed which
allows plugin maintainers to ship a <code>.unitypackage</code> that can provide access
to their own UPM registry server to make it easier for developers to
manage their plugins.</p>
<h2><a id="user-content-unity-plugin-version-management" class="anchor" aria-hidden="true" href="#unity-plugin-version-management"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Unity Plugin Version Management</h2>
<p>Finally, the <em>Version Handler</em> component of this plugin simplifies the process
of managing transitive dependencies of Unity plugins and each plugin's upgrade
process.</p>
<p>For example, without the Version Handler plugin, if:</p>
<ul>
<li>Unity plugin <code>SomePlugin</code> includes <code>EDM4U</code> plugin at
version 1.1.</li>
<li>Unity plugin <code>SomeOtherPlugin</code> includes <code>EDM4U</code>
plugin  at version 1.2.</li>
</ul>
<p>The version of <code>EDM4U</code> included in the developer's project depends upon the
order the developer imports <code>SomePlugin</code> or <code>SomeOtherPlugin</code>.</p>
<p>This results in:</p>
<ul>
<li><code>EDM4U</code> at version 1.2, if <code>SomePlugin</code> is imported then <code>SomeOtherPlugin</code>
is imported.</li>
<li><code>EDM4U</code> at version 1.1, if <code>SomeOtherPlugin</code> is imported then
<code>SomePlugin</code> is imported.</li>
</ul>
<p>The Version Handler solves the problem of managing transitive dependencies by:</p>
<ul>
<li>Specifying a set of packaging requirements that enable a plugin at
different versions to be imported into a Unity project.</li>
<li>Providing activation logic that selects the latest version of a plugin
within a project.</li>
</ul>
<p>When using the Version Handler to manage <code>EDM4U</code> included in <code>SomePlugin</code> and
<code>SomeOtherPlugin</code>, from the prior example, version 1.2 will always be the
version activated in a developer's Unity project.</p>
<p>Plugin creators are encouraged to adopt this library to ease integration for
their customers.  For more information about integrating EDM4U
into your own plugin, see the <a href="#plugin-redistribution">Plugin Redistribution</a>
section of this document.</p>
<h1><a id="user-content-requirements" class="anchor" aria-hidden="true" href="#requirements"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Requirements</h1>
<p>The <em>Android Resolver</em> and <em>iOS Resolver</em> components of the plugin only work
with Unity version 4.6.8 or higher.</p>
<p>The <em>Version Handler</em> component only works with Unity 5.x or higher as it
depends upon the <code>PluginImporter</code> UnityEditor API.</p>
<p>The <em>Unity Package Manager Resolver</em> component only works with
Unity 2018.4 or above, when
<a href="https://docs.unity3d.com/Manual/upm-scoped.html" rel="nofollow">scoped registry</a>
support was added to the Unity Package Manager.</p>
<h1><a id="user-content-getting-started" class="anchor" aria-hidden="true" href="#getting-started"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Getting Started</h1>
<p>Before you import EDM4U into your plugin project, you first
need to consider whether you intend to <em>redistribute</em> <code>EDM4U</code>
along with your own plugin.</p>
<h2><a id="user-content-plugin-redistribution" class="anchor" aria-hidden="true" href="#plugin-redistribution"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Plugin Redistribution</h2>
<p>If you're a plugin maintainer, redistributing <code>EDM4U</code> inside your own plugin
will ease the integration process for your users, by resolving dependency
conflicts between your plugin and other plugins in a user's project.</p>
<p>If you wish to redistribute <code>EDM4U</code> inside your plugin,
you <strong>must</strong> follow these steps when importing the
<code>external-dependency-manager-*.unitypackage</code>, and when exporting your own plugin
package:</p>
<ol>
<li>Import the <code>external-dependency-manager-*.unitypackage</code> into your plugin
project by
<a href="https://docs.unity3d.com/Manual/CommandLineArguments.html" rel="nofollow">running Unity from the command line</a>, ensuring that
you add the <code>-gvh_disable</code> option.</li>
<li>Export your plugin by <a href="https://docs.unity3d.com/Manual/CommandLineArguments.html" rel="nofollow">running Unity from the command line</a>, ensuring that
you:
<ul>
<li>Include the contents of the <code>Assets/PlayServicesResolver</code> directory.</li>
<li>Add the <code>-gvh_disable</code> option.</li>
</ul>
</li>
</ol>
<p>You <strong>must</strong> specify the <code>-gvh_disable</code> option in order for the Version
Handler to work correctly!</p>
<p>For example, the following command will import the
<code>external-dependency-manager-1.2.46.0.unitypackage</code> into the project
<code>MyPluginProject</code> and export the entire Assets folder to
<code>MyPlugin.unitypackage</code>:</p>
<pre><code>Unity -gvh_disable \
      -batchmode \
      -importPackage external-dependency-manager-1.2.46.0.unitypackage \
      -projectPath MyPluginProject \
      -exportPackage Assets MyPlugin.unitypackage \
      -quit
</code></pre>
<h3><a id="user-content-background-1" class="anchor" aria-hidden="true" href="#background-1"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Background</h3>
<p>The <em>Version Handler</em> component relies upon deferring the load of editor DLLs
so that it can run first and determine the latest version of a plugin component
to activate.  The build of <code>EDM4U</code> plugin has Unity asset metadata that is
configured so that the editor components are not initially enabled when it's
imported into a Unity project.  To maintain this configuration when importing
the <code>external-dependency-manager.unitypackage</code> into a Unity plugin project, you
<em>must</em> specify the command line option <code>-gvh_disable</code> which will prevent the
Version Handler component from running and changing the Unity asset metadata.</p>
<h1><a id="user-content-android-resolver-usage" class="anchor" aria-hidden="true" href="#android-resolver-usage"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Android Resolver Usage</h1>
<p>The Android Resolver copies specified dependencies from local or remote Maven
repositories into the Unity project when a user selects Android as the build
target in the Unity editor.</p>
<ol>
<li>
<p>Add the <code>external-dependency-manager-*.unitypackage</code> to your plugin
project (assuming you are developing a plugin). If you are redistributing
EDM4U with your plugin, you <strong>must</strong> follow the
import steps in the <a href="#getting-started">Getting Started</a> section!</p>
</li>
<li>
<p>Copy and rename the
<a href="https://github.com/googlesamples/unity-jar-resolver/blob/master/sample/Assets/ExternalDependencyManager/Editor/SampleDependencies.xml">SampleDependencies.xml</a>
file into your plugin and add the dependencies your plugin requires.</p>
<p>The XML file just needs to be under an <code>Editor</code> directory and match the
name <code>*Dependencies.xml</code>. For example,
<code>MyPlugin/Editor/MyPluginDependencies.xml</code>.</p>
</li>
<li>
<p>Follow the steps in the <a href="#getting-started">Getting Started</a>
section when you are exporting your plugin package.</p>
</li>
</ol>
<p>For example, to add the Google Play Games library
(<code>com.google.android.gms:play-services-games</code> package) at version <code>9.8.0</code> to
the set of a plugin's Android dependencies:</p>
<pre><code>&lt;dependencies&gt;
  &lt;androidPackages&gt;
    &lt;androidPackage spec="com.google.android.gms:play-services-games:9.8.0"&gt;
      &lt;androidSdkPackageIds&gt;
        &lt;androidSdkPackageId&gt;extra-google-m2repository&lt;/androidSdkPackageId&gt;
      &lt;/androidSdkPackageIds&gt;
    &lt;/androidPackage&gt;
  &lt;/androidPackages&gt;
&lt;/dependencies&gt;
</code></pre>
<p>The version specification (last component) supports:</p>
<ul>
<li>Specific versions e.g <code>9.8.0</code></li>
<li>Partial matches e.g <code>9.8.+</code> would match 9.8.0, 9.8.1 etc. choosing the most
recent version.</li>
<li>Latest version using <code>LATEST</code> or <code>+</code>.  We do <em>not</em> recommend using this
unless you're 100% sure the library you depend upon will not break your
Unity plugin in future.</li>
</ul>
<p>The above example specifies the dependency as a component of the Android SDK
manager such that the Android SDK manager will be executed to install the
package if it's not found.  If your Android dependency is located on Maven
central it's possible to specify the package simply using the <code>androidPackage</code>
element:</p>
<pre><code>&lt;dependencies&gt;
  &lt;androidPackages&gt;
    &lt;androidPackage spec="com.google.api-client:google-api-client-android:1.22.0" /&gt;
  &lt;/androidPackages&gt;
&lt;/dependencies&gt;
</code></pre>
<h2><a id="user-content-auto-resolution" class="anchor" aria-hidden="true" href="#auto-resolution"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Auto-resolution</h2>
<p>By default the Android Resolver automatically monitors the dependencies you have
specified and the <code>Plugins/Android</code> folder of your Unity project.  The
resolution process runs when the specified dependencies are not present in your
project.</p>
<p>The <em>auto-resolution</em> process can be disabled via the
<code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Settings</code> menu.</p>
<p>Manual resolution can be performed using the following menu options:</p>
<ul>
<li><code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Resolve</code></li>
<li><code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Force Resolve</code></li>
</ul>
<h2><a id="user-content-deleting-libraries" class="anchor" aria-hidden="true" href="#deleting-libraries"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Deleting libraries</h2>
<p>Resolved packages are tracked via asset labels by the Android Resolver.
They can easily be deleted using the
<code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Delete Resolved Libraries</code>
menu item.</p>
<h2><a id="user-content-android-manifest-variable-processing" class="anchor" aria-hidden="true" href="#android-manifest-variable-processing"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Android Manifest Variable Processing</h2>
<p>Some AAR files (for example play-services-measurement) contain variables that
are processed by the Android Gradle plugin.  Unfortunately, Unity does not
perform the same processing when using Unity's Internal Build System, so the
Android Resolver plugin handles known cases of this variable substitution
by exploding the AAR into a folder and replacing <code>${applicationId}</code> with the
<code>bundleID</code>.</p>
<p>Disabling AAR explosion and therefore Android manifest processing can be done
via the <code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Settings</code>
menu. You may want to disable explosion of AARs if you're exporting a project
to be built with Gradle / Android Studio.</p>
<h2><a id="user-content-abi-stripping" class="anchor" aria-hidden="true" href="#abi-stripping"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>ABI Stripping</h2>
<p>Some AAR files contain native libraries (.so files) for each ABI supported
by Android.  Unfortunately, when targeting a single ABI (e.g x86), Unity does
not strip native libraries for unused ABIs.  To strip unused ABIs, the Android
Resolver plugin explodes an AAR into a folder and removes unused ABIs to
reduce the built APK size.  Furthermore, if native libraries are not stripped
from an APK (e.g you have a mix of Unity's x86 library and some armeabi-v7a
libraries) Android may attempt to load the wrong library for the current
runtime ABI completely breaking your plugin when targeting some architectures.</p>
<p>AAR explosion and therefore ABI stripping can be disabled via the
<code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Settings</code> menu.
You may want to disable explosion of AARs if you're exporting a project to be
built with Gradle / Android Studio.</p>
<h2><a id="user-content-resolution-strategies" class="anchor" aria-hidden="true" href="#resolution-strategies"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Resolution Strategies</h2>
<p>By default the Android Resolver will use Gradle to download dependencies prior
to integrating them into a Unity project.  This works with Unity's internal
build system and Gradle / Android Studio project export.</p>
<p>It's possible to change the resolution strategy via the
<code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Settings</code> menu.</p>
<h3><a id="user-content-download-artifacts-with-gradle" class="anchor" aria-hidden="true" href="#download-artifacts-with-gradle"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Download Artifacts with Gradle</h3>
<p>Using the default resolution strategy, the Android resolver executes the
following operations:</p>
<ul>
<li>Remove the result of previous Android resolutions.
e.g Delete all files and directories labeled with "gpsr" under
<code>Plugins/Android</code> from the project.</li>
<li>Collect the set of Android dependencies (libraries) specified by a
project's <code>*Dependencies.xml</code> files.</li>
<li>Run <code>download_artifacts.gradle</code> with Gradle to resolve conflicts and,
if successful, download the set of resolved Android libraries (AARs, JARs).</li>
<li>Process each AAR / JAR so that it can be used with the currently selected
Unity build system (e.g Internal vs. Gradle, Export vs. No Export).
This involves patching each reference to <code>applicationId</code> in the
AndroidManifest.xml with the project's bundle ID.  This means resolution
must be run if the bundle ID is changed again.</li>
<li>Move the processed AARs to <code>Plugins/Android</code> so they will be included when
Unity invokes the Android build.</li>
</ul>
<h3><a id="user-content-integrate-into-maintemplategradle" class="anchor" aria-hidden="true" href="#integrate-into-maintemplategradle"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Integrate into mainTemplate.gradle</h3>
<p>Unity 5.6 introduced support for customizing the <code>build.gradle</code> used to build
Unity projects with Gradle. When the <em>Patch mainTemplate.gradle</em> setting is
enabled, rather than downloading artifacts before the build, Android resolution
results in the execution of the following operations:</p>
<ul>
<li>Remove the result of previous Android resolutions.
e.g Delete all files and directories labeled with "gpsr" under
<code>Plugins/Android</code> from the project and remove sections delimited with
<code>// Android Resolver * Start</code> and <code>// Android Resolver * End</code> lines.</li>
<li>Collect the set of Android dependencies (libraries) specified by a
project's <code>*Dependencies.xml</code> files.</li>
<li>Rename any <code>.srcaar</code> files in the build to <code>.aar</code> and exclude them from
being included directly by Unity in the Android build as
<code>mainTemplate.gradle</code> will be patched to include them instead from their
local maven repositories.</li>
<li>Inject the required Gradle repositories into <code>mainTemplate.gradle</code> at the
line matching the pattern
<code>.*apply plugin: 'com\.android\.(application|library)'.*</code> or the section
starting at the line <code>// Android Resolver Repos Start</code>.
If you want to control the injection point in the file, the section
delimited by the lines <code>// Android Resolver Repos Start</code> and
<code>// Android Resolver Repos End</code> should be placed in the global scope
before the <code>dependencies</code> section.</li>
<li>Inject the required Android dependencies (libraries) into
<code>mainTemplate.gradle</code> at the line matching the pattern <code>***DEPS***</code> or
the section starting at the line <code>// Android Resolver Dependencies Start</code>.
If you want to control the injection point in the file, the section
delimited by the lines <code>// Android Resolver Dependencies Start</code> and
<code>// Android Resolver Dependencies End</code> should be placed in the
<code>dependencies</code> section.</li>
<li>Inject the packaging options logic, which excludes architecture specific
libraries based upon the selected build target, into <code>mainTemplate.gradle</code>
at the line matching the pattern <code>android +{</code> or the section starting at
the line <code>// Android Resolver Exclusions Start</code>.
If you want to control the injection point in the file, the section
delimited by the lines <code>// Android Resolver Exclusions Start</code> and
<code>// Android Resolver Exclusions End</code> should be placed in the global
scope before the <code>android</code> section.</li>
</ul>
<h2><a id="user-content-dependency-tracking" class="anchor" aria-hidden="true" href="#dependency-tracking"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Dependency Tracking</h2>
<p>The Android Resolver creates the
<code>ProjectSettings/AndroidResolverDependencies.xml</code> to quickly determine the set
of resolved dependencies in a project.  This is used by the auto-resolution
process to only run the expensive resolution process when necessary.</p>
<h2><a id="user-content-displaying-dependencies" class="anchor" aria-hidden="true" href="#displaying-dependencies"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Displaying Dependencies</h2>
<p>It's possible to display the set of dependencies the Android Resolver
would download and process in your project via the
<code>Assets &gt; External Dependency Manager &gt; Android Resolver &gt; Display Libraries</code>
menu item.</p>
<h1><a id="user-content-ios-resolver-usage" class="anchor" aria-hidden="true" href="#ios-resolver-usage"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>iOS Resolver Usage</h1>
<p>The iOS resolver component of this plugin manages
<a href="https://cocoapods.org/" rel="nofollow">CocoaPods</a>.  A CocoaPods <code>Podfile</code> is generated and
the <code>pod</code> tool is executed as a post build process step to add dependencies
to the Xcode project exported by Unity.</p>
<p>Dependencies for iOS are added by referring to CocoaPods.</p>
<ol>
<li>
<p>Add the <code>external-dependency-manager-*.unitypackage</code> to your plugin
project (assuming you are developing a plugin). If you are redistributing
EDM4U with your plugin, you <strong>must</strong> follow the
import steps in the <a href="#getting-started">Getting Started</a> section!</p>
</li>
<li>
<p>Copy and rename the
<a href="https://github.com/googlesamples/unity-jar-resolver/blob/master/sample/Assets/ExternalDependencyManager/Editor/SampleDependencies.xml">SampleDependencies.xml</a>
file into your plugin and add the dependencies your plugin requires.</p>
<p>The XML file just needs to be under an <code>Editor</code> directory and match the
name <code>*Dependencies.xml</code>. For example,
<code>MyPlugin/Editor/MyPluginDependencies.xml</code>.</p>
</li>
<li>
<p>Follow the steps in the <a href="#getting-started">Getting Started</a>
section when you are exporting your plugin package.</p>
</li>
</ol>
<p>For example, to add the AdMob pod, version 7.0 or greater with bitcode enabled:</p>
<pre><code>&lt;dependencies&gt;
  &lt;iosPods&gt;
    &lt;iosPod name="Google-Mobile-Ads-SDK" version="~&gt; 7.0" bitcodeEnabled="true"
            minTargetSdk="6.0" /&gt;
  &lt;/iosPods&gt;
&lt;/dependencies&gt;
</code></pre>
<h2><a id="user-content-integration-strategies" class="anchor" aria-hidden="true" href="#integration-strategies"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Integration Strategies</h2>
<p>The <code>CocoaPods</code> are either:</p>
<ul>
<li>Downloaded and injected into the Xcode project file directly, rather than
creating a separate xcworkspace.  We call this <code>Xcode project</code> integration.</li>
<li>If the Unity version supports opening a xcworkspace file, the <code>pod</code> tool
is used as intended to generate a xcworkspace which references the
CocoaPods.  We call this <code>Xcode workspace</code> integration.</li>
</ul>
<p>The resolution strategy can be changed via the
<code>Assets &gt; External Dependency Manager &gt; iOS Resolver &gt; Settings</code> menu.</p>
<h3><a id="user-content-appending-text-to-generated-podfile" class="anchor" aria-hidden="true" href="#appending-text-to-generated-podfile"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Appending text to generated Podfile</h3>
<p>In order to modify the generated Podfile you can create a script like this:</p>
<pre><code>using System.IO;
public class PostProcessIOS : MonoBehaviour {
[PostProcessBuildAttribute(45)]//must be between 40 and 50 to ensure that it's not overriden by Podfile generation (40) and that it's added before "pod install" (50)
private static void PostProcessBuild_iOS(BuildTarget target, string buildPath)
{
    if (target == BuildTarget.iOS)
    {

        using (StreamWriter sw = File.AppendText(buildPath + "/Podfile"))
        {
            //in this example I'm adding an app extension
            sw.WriteLine("\ntarget 'NSExtension' do\n  pod 'Firebase/Messaging', '6.6.0'\nend");
        }
    }
}
</code></pre>
<h1><a id="user-content-unity-package-manager-resolver-usage" class="anchor" aria-hidden="true" href="#unity-package-manager-resolver-usage"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Unity Package Manager Resolver Usage</h1>
<p>Adding registries to the
<a href="https://docs.unity3d.com/Manual/Packages.html" rel="nofollow">Unity Package Manager</a>
(UPM) is a manual process. The Unity Package Manager Resolver (UPMR) component
of this plugin makes it easy for plugin maintainers to distribute new UPM
registry servers and easy for plugin users to manage UPM registry servers.</p>
<h2><a id="user-content-adding-registries" class="anchor" aria-hidden="true" href="#adding-registries"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Adding Registries</h2>
<ol>
<li>
<p>Add the <code>external-dependency-manager-*.unitypackage</code> to your plugin
project (assuming you are developing a plugin). If you are redistributing
EDM4U with your plugin, you <strong>must</strong> follow the
import steps in the <a href="#getting-started">Getting Started</a> section!</p>
</li>
<li>
<p>Copy and rename the
<a href="https://github.com/googlesamples/unity-jar-resolver/blob/master/sample/Assets/ExternalDependencyManager/Editor/sample/Assets/ExternalDependencyManager/Editor/SampleRegistries.xml">SampleRegistries.xml</a>
file into your plugin and add the registries your plugin requires.</p>
<p>The XML file just needs to be under an <code>Editor</code> directory and match the
name <code>*Registries.xml</code> or labeled with <code>gumpr_registries</code>. For example,
<code>MyPlugin/Editor/MyPluginRegistries.xml</code>.</p>
</li>
<li>
<p>Follow the steps in the <a href="#getting-started">Getting Started</a>
section when you are exporting your plugin package.</p>
</li>
</ol>
<p>For example, to add a registry for plugins in the scope <code>com.coolstuff</code>:</p>
<pre><code>&lt;registries&gt;
  &lt;registry name="Cool Stuff"
            url="https://unityregistry.coolstuff.com"
            termsOfService="https://coolstuff.com/unityregistry/terms"
            privacyPolicy="https://coolstuff.com/unityregistry/privacy"&gt;
    &lt;scopes&gt;
      &lt;scope&gt;com.coolstuff&lt;/scope&gt;
    &lt;/scopes&gt;
  &lt;/registry&gt;
&lt;/registries&gt;
</code></pre>
<p>When UPMR is loaded it will prompt the developer to add the registry to their
project if it isn't already present in the <code>Packages/manifest.json</code> file.</p>
<p>For more information, see Unity's documentation on
<a href="https://docs.unity3d.com/Manual/upm-scoped.html" rel="nofollow">scoped package registries</a>.</p>
<h2><a id="user-content-managing-registries" class="anchor" aria-hidden="true" href="#managing-registries"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Managing Registries</h2>
<p>It's possible to add and remove registries that are specified via UPMR
XML configuration files via the following menu options:</p>
<ul>
<li><code>Assets &gt; External Dependency Manager &gt; Unity Package Manager Resolver &gt; Add Registries</code> will prompt the user with a window which allows them to
add registries discovered in the project to the Unity Package Manager.</li>
<li><code>Assets &gt; External Dependency Manager &gt; Unity Package Manager Resolver &gt; Remove Registries</code> will prompt the user with a window which allows them to
remove registries discovered in the project from the Unity Package Manager.</li>
<li><code>Assets &gt; External Dependency Manager &gt; Unity Package Manager Resolver &gt; Modify Registries</code> will prompt the user with a window which allows them to
add or remove registries discovered in the project.</li>
</ul>
<h2><a id="user-content-configuration" class="anchor" aria-hidden="true" href="#configuration"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Configuration</h2>
<p>UPMR can be configured via the <code>Assets &gt; External Dependency Manager &gt; Unity Package Manager Resolver &gt; Settings</code> menu option:</p>
<ul>
<li><code>Add package registries</code> when enabled, when the plugin loads or registry
configuration files change, this will prompt the user to add registries
that are not present in the Unity Package Manager.</li>
<li><code>Prompt to add package registries</code> will cause a developer to be prompted
with a window that will ask for confirmation before adding registries.
When this is disabled registries are added silently to the project.</li>
<li><code>Enable Analytics Reporting</code> when enabled, reports the use of the plugin
to the developers so they can make imrpovements.</li>
<li><code>Verbose logging</code> when enabled prints debug information to the console
which can be useful when filing bug reports.</li>
</ul>
<h1><a id="user-content-version-handler-usage" class="anchor" aria-hidden="true" href="#version-handler-usage"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Version Handler Usage</h1>
<p>The Version Handler component of this plugin manages:</p>
<ul>
<li>Shared Unity plugin dependencies.</li>
<li>Upgrading Unity plugins by cleaning up old files from previous versions.</li>
<li>Uninstallation of plugins that are distributed with manifest files.</li>
<li>Restoration of plugin assets to their original install locations if assets
are tagged with the <code>exportpath</code> label.</li>
</ul>
<p>Since the Version Handler needs to modify Unity asset metadata (<code>.meta</code> files),
to enable / disable components, rename and delete asset files it does not
work with Unity Package Manager installed packages. It's still possible to
include EDM4U in Unity Package Manager packages, the Version Handler component
simply won't do anything to UPM plugins in this case.</p>
<h2><a id="user-content-using-version-handler-managed-plugins" class="anchor" aria-hidden="true" href="#using-version-handler-managed-plugins"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Using Version Handler Managed Plugins</h2>
<p>If a plugin is imported at multiple different versions into a project, if
the Version Handler is enabled, it will automatically check all managed
assets to determine the set of assets that are out of date and assets that
should be removed. To disable automatic checking managed assets disable
the <code>Enable version management</code> option in the
<code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Settings</code> menu.</p>
<p>If version management is disabled, it's possible to check managed assets
manually using the
<code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Update</code> menu option.</p>
<h3><a id="user-content-listing-managed-plugins" class="anchor" aria-hidden="true" href="#listing-managed-plugins"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Listing Managed Plugins</h3>
<p>Plugins managed by the Version Handler, those that ship with manifest files,
can displayed using the <code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Display Managed Packages</code> menu option. The list of plugins
are written to the console window along with the set of files used by each
plugin.</p>
<h3><a id="user-content-uninstalling-managed-plugins" class="anchor" aria-hidden="true" href="#uninstalling-managed-plugins"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Uninstalling Managed Plugins</h3>
<p>Plugins managed by the Version Handler, those that ship with manifest files,
can be removed using the <code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Uninstall Managed Packages</code> menu option. This operation
will display a window that allows a developer to select a set of plugins to
remove which will remove all files owned by each plugin excluding those that
are in use by other installed plugins.</p>
<p>Files managed by the Version Handler, those labeled with the <code>gvh</code> asset label,
can be checked to see whether anything needs to be upgraded, disabled or
removed using the <code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Update</code> menu option.</p>
<h3><a id="user-content-restore-install-paths" class="anchor" aria-hidden="true" href="#restore-install-paths"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Restore Install Paths</h3>
<p>Some developers move assets around in their project which can make it
harder for plugin maintainers to debug issues if this breaks Unity's
<a href="https://docs.unity3d.com/Manual/SpecialFolders.html" rel="nofollow">special folders</a> rules.
If assets are labeled with their original install / export path
(see <code>gvhp_exportpath</code> below), Version Handler can restore assets to their
original locations when using the <code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Move Files To Install Locations</code> menu option.</p>
<h3><a id="user-content-settings" class="anchor" aria-hidden="true" href="#settings"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Settings</h3>
<p>Some behavior of the Version Handler can be configured via the
<code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Settings</code> menu
option.</p>
<ul>
<li><code>Enable version management</code> controls whether the plugin should automatically
check asset versions and apply changes. If this is disabled the process
should be run manually when installing or upgrading managed plugins using
<code>Assets &gt; External Dependency Manager &gt; Version Handler &gt; Update</code>.</li>
<li><code>Rename to canonical filenames</code> is a legacy option that will rename files to
remove version numbers and other labels from filenames.</li>
<li><code>Prompt for obsolete file deletion</code> enables the display of a window when
obsolete files are deleted allowing the developer to select which files to
delete and those to keep.</li>
<li><code>Allow disabling files via renaming</code> controls whether obsolete or disabled
files should be disabled by renaming them to <code>myfilename_DISABLED</code>.
Renaming to disable files is required in some scenarios where Unity doesn't
support removing files from the build via the PluginImporter.</li>
<li><code>Enable Analytics Reporting</code> enables / disables usage reporting to plugin
developers to improve the product.</li>
<li><code>Verbose logging</code> enables <em>very</em> noisy log output that is useful for
debugging while filing a bug report or building a new managed plugin.</li>
<li><code>Use project settings</code> saves settings for the plugin in the project rather
than system-wide.</li>
</ul>
<h2><a id="user-content-redistributing-a-managed-plugin" class="anchor" aria-hidden="true" href="#redistributing-a-managed-plugin"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Redistributing a Managed Plugin</h2>
<p>The Version Handler employs a couple of methods for managing version
selection, upgrade and removal of plugins.</p>
<ul>
<li>Each plugin can ship with a manifest file that lists the files it includes.
This makes it possible for Version Handler to calculate the difference
in assets between the most recent release of a plugin and the previous
release installed in a project. If a files are removed the Version Handler
will prompt the user to clean up obsolete files.</li>
<li>Plugins can ship using assets with unique names, unique GUIDs and version
number labels. Version numbers can be attached to assets using labels or
added to the filename (e.g <code>myfile.txt</code> would be `myfile_version-x.y.z.txt).
This allows the Version Handler to determine which set of files are the
same file at different versions, select the most recent version and prompt
the developer to clean up old versions.</li>
</ul>
<p>Unity plugins can be managed by the Version Handler using the following steps:</p>
<ol>
<li>Add the <code>gvh</code> asset label to each asset (file) you want Version Handler
to manage.</li>
<li>Add the <code>gvh_version-VERSION</code> label to each asset where <code>VERSION</code> is the
version of the plugin you're releasing (e.g 1.2.3).</li>
<li>Add the <code>gvhp_exportpath-PATH</code> label to each asset where <code>PATH</code> is the
export path of the file when the <code>.unitypackage</code> is created.  This is
used to track files if they're moved around in a project by developers.</li>
<li>Optional: Add <code>gvh_targets-editor</code> label to each editor DLL in your
plugin and disable <code>editor</code> as a target platform for the DLL.
The Version Handler will enable the most recent version of this DLL when
the plugin is imported.</li>
<li>Optional: If your plugin is included in other Unity plugins, you should
add the version number to each filename and change the GUID of each asset.
This allows multiple versions of your plugin to be imported into a Unity
project, with the Version Handler component activating only the most
recent version.</li>
<li>Create a manifest text file named <code>MY_UNIQUE_PLUGIN_NAME_VERSION.txt</code>
that lists all the files in your plugin relative to the project root.
Then add the <code>gvh_manifest</code> label to the asset to indicate this file is
a plugin manifest.</li>
<li>Optional: Add a <code>gvhp_manifestname-NAME</code> label to your manifest file
to provide a human readable name for your package.  If this isn't provided
the name of the manifest file will be used as the package name.
NAME can match the pattern <code>[0-9]+[a-zA-Z -]' where a leading integer will set the priority of the name where </code>0` is the highest priority
and preferably used as the display name. The lowest value (i.e highest
priority name) will be used as the display name and all other specified
names will be aliases of the display name. Aliases can refer to previous
names of the package allowing renaming across published versions.</li>
<li>Redistribute EDM4U Unity plugin with your plugin.
See the <a href="#plugin-redistribution">Plugin Redistribution</a> for the details.</li>
</ol>
<p>If you follow these steps:</p>
<ul>
<li>When users import a newer version of your plugin, files referenced by the
older version's manifest are cleaned up.</li>
<li>The latest version of the plugin will be selected when users import
multiple packages that include your plugin, assuming the steps in
<a href="#plugin-redistribution">Plugin Redistribution</a> are followed.</li>
</ul>
<h1><a id="user-content-building-from-source" class="anchor" aria-hidden="true" href="#building-from-source"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Building from Source</h1>
<p>To build this plugin from source you need the following tools installed:</p>
<ul>
<li>Unity (with iOS and Android modules installed)</li>
</ul>
<p>You can build the plugin by running the following from your shell
(Linux / OSX):</p>
<pre><code>./gradlew build
</code></pre>
<p>or Windows:</p>
<pre><code>./gradlew.bat build
</code></pre>
<h1><a id="user-content-releasing" class="anchor" aria-hidden="true" href="#releasing"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Releasing</h1>
<p>Each time a new build of this plugin is checked into the source tree you
need to do the following:</p>
<ul>
<li>Bump the plugin version variable <code>pluginVersion</code> in <code>build.gradle</code></li>
<li>Update <code>CHANGELOG.md</code> with the new version number and changes included in
the release.</li>
<li>Build the release using <code>./gradle release</code> which performs the following:
<ul>
<li>Updates <code>external-dependency-manager-*.unitypackage</code></li>
<li>Copies the unpacked plugin to the <code>exploded</code> directory.</li>
<li>Updates template metadata files in the <code>plugin</code> directory.
The GUIDs of all asset metadata is modified due to the version number
change. Each file within the plugin is versioned to allow multiple
versions of the plugin to be imported into a Unity project which allows
the most recent version to be activated by the Version Handler
component.</li>
</ul>
</li>
<li>Create the release commit and tag the release using
<code>./gradle gitTagRelease</code> which performs the following:
<ul>
<li><code>git add -A</code> to pick up all modified, new and deleted files in the tree.</li>
<li><code>git commit --amend -a</code> to create a release commit with the release notes
in the change log.</li>
<li><code>git tag -a RELEASE -m "version RELEASE"</code> to tag the release.</li>
</ul>
</li>
</ul>
</article>
      </div>
  </div>




  </div>
</div>

    </main>
  </div>
  

  </div>

        
<div class="footer container-lg width-full px-3" role="contentinfo">
  <div class="position-relative d-flex flex-justify-between pt-6 pb-2 mt-6 f6 text-gray border-top border-gray-light ">
    <ul class="list-style-none d-flex flex-wrap ">
      <li class="mr-3">&copy; 2020 GitHub, Inc.</li>
        <li class="mr-3"><a data-ga-click="Footer, go to terms, text:terms" href="https://github.com/site/terms">Terms</a></li>
        <li class="mr-3"><a data-ga-click="Footer, go to privacy, text:privacy" href="https://github.com/site/privacy">Privacy</a></li>
        <li class="mr-3"><a data-ga-click="Footer, go to security, text:security" href="https://github.com/security">Security</a></li>
        <li class="mr-3"><a href="https://githubstatus.com/" data-ga-click="Footer, go to status, text:status">Status</a></li>
        <li><a data-ga-click="Footer, go to help, text:help" href="https://help.github.com">Help</a></li>

    </ul>

    <a aria-label="Homepage" title="GitHub" class="footer-octicon d-none d-lg-block mx-lg-4" href="https://github.com">
      <svg height="24" class="octicon octicon-mark-github" viewBox="0 0 16 16" version="1.1" width="24" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z"/></svg>
</a>
   <ul class="list-style-none d-flex flex-wrap ">
        <li class="mr-3"><a data-ga-click="Footer, go to contact, text:contact" href="https://github.com/contact">Contact GitHub</a></li>
        <li class="mr-3"><a href="https://github.com/pricing" data-ga-click="Footer, go to Pricing, text:Pricing">Pricing</a></li>
      <li class="mr-3"><a href="https://developer.github.com" data-ga-click="Footer, go to api, text:api">API</a></li>
      <li class="mr-3"><a href="https://training.github.com" data-ga-click="Footer, go to training, text:training">Training</a></li>
        <li class="mr-3"><a href="https://github.blog" data-ga-click="Footer, go to blog, text:blog">Blog</a></li>
        <li><a data-ga-click="Footer, go to about, text:about" href="https://github.com/about">About</a></li>
    </ul>
  </div>
  <div class="d-flex flex-justify-center pb-6">
    <span class="f6 text-gray-light"></span>
  </div>
</div>



  <div id="ajax-error-message" class="ajax-error-message flash flash-error">
    <svg class="octicon octicon-alert" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 000 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 00.01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"/></svg>
    <button type="button" class="flash-close js-ajax-error-dismiss" aria-label="Dismiss error">
      <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
    </button>
    You can’t perform that action at this time.
  </div>


    <script crossorigin="anonymous" async="async" integrity="sha512-o4vS4IKrjdy/HD+xr2+VhO6DxQmj5jikhHbEGrd8+JGhpmIOxRrpT1Qo5k3IhKimm8VXIu3pyYejLtOAkm+OsQ==" type="application/javascript" id="js-conditional-compat" data-src="https://github.githubassets.com/assets/compat-bootstrap-a38bd2e0.js"></script>
    <script crossorigin="anonymous" integrity="sha512-6XqOrpzsRfeWz1MuH9q2GuzW4Ktvt+kA5KbChOp1ZjaoGbRl3tBng8HiA5B/lClMvkkj4h+vVuSwLeh14JzGuA==" type="application/javascript" src="https://github.githubassets.com/assets/environment-bootstrap-e97a8eae.js"></script>
    <script crossorigin="anonymous" async="async" integrity="sha512-1/6VVx6z9r6uphSoGlmYgbqD5KaY+GVMt1Gqa3DIa0U+3Pv2SWu8Fk1BZ2xPne5upvF8HdEWcGeiUjd2URl+oA==" type="application/javascript" src="https://github.githubassets.com/assets/vendor-d7fe9557.js"></script>
    <script crossorigin="anonymous" async="async" integrity="sha512-RDggGUYWJq0pjfr/60y4ITVdK0zFDicDs0h46xLtUfsgDq6L6OWInB0F66615pE295U74v8ykPH/qsoL3U38ng==" type="application/javascript" src="https://github.githubassets.com/assets/frameworks-44382019.js"></script>
    
    <script crossorigin="anonymous" async="async" integrity="sha512-oW477oJbyGa7p4PDjXTSATlfpih868M2V4/M3CQKRkWWOaRwSmM2CQSwG3+Ap3OhMOzKPlXebSQvugpVTs5XZQ==" type="application/javascript" src="https://github.githubassets.com/assets/github-bootstrap-a16e3bee.js"></script>
    
    
    
  <div class="js-stale-session-flash flash flash-warn flash-banner" hidden
    >
    <svg class="octicon octicon-alert" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 000 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 00.01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"/></svg>
    <span class="js-stale-session-flash-signed-in" hidden>You signed in with another tab or window. <a href="">Reload</a> to refresh your session.</span>
    <span class="js-stale-session-flash-signed-out" hidden>You signed out in another tab or window. <a href="">Reload</a> to refresh your session.</span>
  </div>
  <template id="site-details-dialog">
  <details class="details-reset details-overlay details-overlay-dark lh-default text-gray-dark hx_rsm" open>
    <summary role="button" aria-label="Close dialog"></summary>
    <details-dialog class="Box Box--overlay d-flex flex-column anim-fade-in fast hx_rsm-dialog hx_rsm-modal">
      <button class="Box-btn-octicon m-0 btn-octicon position-absolute right-0 top-0" type="button" aria-label="Close dialog" data-close-dialog>
        <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
      </button>
      <div class="octocat-spinner my-6 js-details-dialog-spinner"></div>
    </details-dialog>
  </details>
</template>

  <div class="Popover js-hovercard-content position-absolute" style="display: none; outline: none;" tabindex="0">
  <div class="Popover-message Popover-message--bottom-left Popover-message--large Box box-shadow-large" style="width:360px;">
  </div>
</div>

  <div aria-live="polite" class="js-global-screen-reader-notice sr-only"></div>

  </body>
</html>

