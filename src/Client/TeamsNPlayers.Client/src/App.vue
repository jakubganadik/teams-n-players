<template>
  <div class="h-screen flex">
    <nav class="w-22 pa-4 flex flex-col gap-5 items-center t-label-medium bg-surface elevation-2">
      <router-link to="/individuals" active-class="active group">
        <i class="icon-face-outline group-[.active]:icon-face" />
        <span>
          Individuals
        </span>
      </router-link>
      <router-link to="/teams" active-class="active group">
        <i class="icon-group-outline group-[.active]:icon-group" />
        <span>
          Teams
        </span>
      </router-link>
      <router-link to="/players" active-class="active group">
        <i class="icon-sports-soccer group-[.active]:icon-sports-soccer" />
        <span>
          Players
        </span>
      </router-link>
    </nav>

    <div class="flex-1 h-screen">
      <div class="h-full overflow-y-scroll scrollbar scrollbar-rounded scrollbar-w-1 scrollbar-track-color-transparent scrollbar-thumb-color-surface-variant">
        <main class="h-full max-w-256 mx-auto">
          <router-view />
        </main>
      </div>
    </div>
  </div>
</template>

<style lang="postcss">
  nav > a { @apply flex flex-col gap-1 h-14 items-center justify-center relative }
  nav > a::before { @apply w-14 h-8 absolute top-0 rounded-4 content-empty }

  nav > a:hover::after { @apply bg-on-primary-container opacity-8 w-14 h-8 absolute top-0 rounded-4 content-empty }

  nav > a > i { @apply text-6 !h-8 }
  nav > a > span { @apply flex-1 flex items-center }

  nav > a.active { @apply text-on-primary-container }
  nav > a.active::before { @apply bg-primary-container w-14 h-8 absolute top-0 rounded-4 content-empty }

</style>
